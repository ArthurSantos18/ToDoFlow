using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Application.Services.Interfaces;
using ToDoFlow.Application.Services.Utils;
using ToDoFlow.Domain.Models;
using ToDoFlow.Domain.Models.Enums;
using ToDoFlow.Infrastructure.Repositories.Interfaces;

namespace ToDoFlow.Application.Services
{
    public class AccountService(IConfiguration configuration, IPasswordService passwordService, ITokenService tokenService,
        IEmailService emailService, IUserRepository userRepository, IMapper mapper, IUserRefreshTokenRepository userRefreshTokenRepository): IAccountService
    {
        
        private readonly IConfiguration _configuration = configuration;
        private readonly IPasswordService _passwordService = passwordService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository = userRefreshTokenRepository;
        
        public async Task<ApiResponse<string, UserRefreshTokenReadDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            User user = await _userRepository.GetUserByEmailAsync(loginRequestDto.Email);

            ValidationHelper.ValidateObject(user, "User");

            if (!_passwordService.VerifyPassword(loginRequestDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid credentials");
            }

            string token = _tokenService.GenerateToken(user, TokenPurpose.Login.ToString());
            
            await _userRefreshTokenRepository.DeleteExpiredTokensByUserIdAsync(user.Id);

            UserRefreshToken? existingRefreshToken = await _userRefreshTokenRepository.GetUserRefreshByUserIdAsync(user.Id);

            if (existingRefreshToken != null)
            {
                await _userRefreshTokenRepository.DeleteUserRefreshTokenAsync(existingRefreshToken.RefreshToken);
            }

            UserRefreshToken newRefreshToken = _tokenService.GenerateRefreshToken(user);

            await _userRefreshTokenRepository.CreateUserRefreshTokenAsync(newRefreshToken);

            UserRefreshTokenReadDto refreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(newRefreshToken);

            return new ApiResponse<string, UserRefreshTokenReadDto>(token, refreshTokenDto, true, "Login successfully", 200);
        }

        public async Task<ApiResponse<string, UserRefreshTokenReadDto>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            User existingUser = await _userRepository.GetUserByEmailAsync(registerRequestDto.Email);

            if (existingUser != null)
            {
                throw new ValidationException("User already exists");
            }

            if (registerRequestDto.Password != registerRequestDto.ConfirmPassword)
            {
                throw new ValidationException("Passwords do not match");
            }

            User user = _mapper.Map<User>(registerRequestDto);
            user.Password = _passwordService.HashPassword(user.Password);

            await _userRepository.CreateUserAsync(user);

            string token = _tokenService.GenerateToken(user, TokenPurpose.Login.ToString());
            UserRefreshToken userRefreshToken = _tokenService.GenerateRefreshToken(user);

            await _userRefreshTokenRepository.CreateUserRefreshTokenAsync(userRefreshToken);

            UserRefreshTokenReadDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(userRefreshToken);

            return new ApiResponse<string, UserRefreshTokenReadDto>(token, userRefreshTokenDto, true, "Registration completed successfully", 200);
        }

        public async Task<ApiResponse<string, UserRefreshTokenReadDto>> RefreshTokenAsync(UserRefreshTokenRefreshDto userRefreshTokenRefreshDto)
        {
            UserRefreshToken? userRefreshToken = await _userRefreshTokenRepository.GetUserRefreshByTokenAsync(userRefreshTokenRefreshDto.RefreshToken);
            ValidationHelper.ValidateObject(userRefreshToken, "Refresh Token");

            User user = await _userRepository.GetUserByIdAsync(userRefreshToken.UserId);
            ValidationHelper.ValidateObject(user, "User");

            var newToken = _tokenService.GenerateToken(user, TokenPurpose.Login.ToString());
            await _userRefreshTokenRepository.DeleteUserRefreshTokenAsync(userRefreshToken.RefreshToken);

            UserRefreshToken newRefreshToken = _tokenService.GenerateRefreshToken(user);
            await _userRefreshTokenRepository.CreateUserRefreshTokenAsync(newRefreshToken);

            UserRefreshTokenReadDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(newRefreshToken);

            return new ApiResponse<string, UserRefreshTokenReadDto>(newToken, userRefreshTokenDto, true, "Operation carried out successfully", 200);
        }

        public async Task<ApiResponse> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            User user = await _userRepository.GetUserByEmailAsync(forgotPasswordDto.Email);

            if (user == null)
            {
                return new ApiResponse(true, $"If the email exists, a reset link was sent", 200);
            }

            string token = _tokenService.GenerateToken(user, TokenPurpose.ResetPassword.ToString());
            string resetLink = $"{_configuration["FrontEnd:Url"]}/reset-password?token={Uri.EscapeDataString(token)}&email={user.Email}";

            bool sendEmail = await _emailService.SendEmailAsync(
                user.Email,
                "Password Reset", 
                $"Click the link to reset your password: <a href='{resetLink}'>Reset Password</a>");

            if (!sendEmail)
            {
                throw new InvalidOperationException($"Failed to send reset email");
            }

            Console.WriteLine(token);

            return new ApiResponse(sendEmail, "Email sent successfully", 200);
        }

        public async Task<ApiResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            ClaimsPrincipal principal = _tokenService.ValidateToken(resetPasswordDto.Token);

            foreach (var claim in principal.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }

            string? userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                throw new ValidationException("Invalid token");
            }

            User user = await _userRepository.GetUserByIdAsync(int.Parse(userId));

            ValidationHelper.ValidateObject(user, "User");
            

            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                throw new ValidationException("Passwords do not match");
            }

            user.Password = _passwordService.HashPassword(resetPasswordDto.NewPassword);

            await _userRepository.UpdateUserAsync(user);

            return new ApiResponse(true, "Password reset successfully", 200);
        }
    }
}

