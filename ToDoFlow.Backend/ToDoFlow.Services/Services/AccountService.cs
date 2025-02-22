using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services
{
    public class AccountService(IConfiguration configuration, IEncryptionService encryptionService, ITokenService tokenService,
        IEmailService emailService, IUserRepository userRepository, IMapper mapper, IUserRefreshTokenRepository userRefreshTokenRepository): IAccountService
    {
        
        private readonly IConfiguration _configuration = configuration;
        private readonly IEncryptionService _encryptionService = encryptionService;
        private readonly ITokenService _tokenService = tokenService;
        private readonly IMapper _mapper = mapper;
        private readonly IEmailService _emailService = emailService;

        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository = userRefreshTokenRepository;
        
        public async Task<ApiResponse<string, UserRefreshTokenReadDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            User user = await _userRepository.ReadUserByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                return new ApiResponse<string, UserRefreshTokenReadDto>(null, null, false, "Erro: User not found", 404);
            }

            if (!_encryptionService.VerifyPassword(loginRequestDto.Password, user.Password))
            {
                return new ApiResponse<string, UserRefreshTokenReadDto>(null, null,false, "Erro: Invalid credentials", 401);
            }

            string token = _tokenService.GenerateToken(user, "login", int.Parse(_configuration["JwtSettings:ExpirationMinutesJwt"]));

            await _userRefreshTokenRepository.DeleteExpiredTokensByUserIdAsync(user.Id);

            UserRefreshToken existingRefreshToken = await _userRefreshTokenRepository.ReadUserRefreshByUserIdAsync(user.Id);

            if (existingRefreshToken == null)
            {
                UserRefreshToken userRefreshToken = _tokenService.GenerateRefreshToken(user);
                await _userRefreshTokenRepository.CreateUserRefreshTokenAsync(userRefreshToken);
                UserRefreshTokenReadDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(userRefreshToken);
                
                return new ApiResponse<string, UserRefreshTokenReadDto>(token, userRefreshTokenDto, true, "Login successfully", 200);
            }
            else
            {
                await _userRefreshTokenRepository.DeleteUserRefreshTokenAsync(existingRefreshToken.RefreshToken);
                
                UserRefreshToken userRefreshToken = _tokenService.GenerateRefreshToken(user);
                await _userRefreshTokenRepository.CreateUserRefreshTokenAsync(userRefreshToken);
                
                UserRefreshTokenReadDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(userRefreshToken);
                return new ApiResponse<string, UserRefreshTokenReadDto>(token, userRefreshTokenDto, true, "Login successfully", 200);
            }
    
        }

        public async Task<ApiResponse<string, UserRefreshTokenReadDto>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            User existingUser = await _userRepository.ReadUserByEmailAsync(registerRequestDto.Email);

            if (existingUser != null)
            {
                return new ApiResponse<string, UserRefreshTokenReadDto>(null, null, false, "Erro: User already exists", 400);
            }

            if (registerRequestDto.Password != registerRequestDto.ConfirmPassword)
            {
                return new ApiResponse<string, UserRefreshTokenReadDto>(null, null, false, "Erro: Passwords do not match", 400);
            }

            try
            {
                User user = _mapper.Map<User>(registerRequestDto);
                user.Password = _encryptionService.HashPassword(user.Password);

                await _userRepository.CreateUserAsync(user);

                string token = _tokenService.GenerateToken(user, "refresh", int.Parse(_configuration["JwtSettings:ExpirationMinutesJwt"]));
                UserRefreshToken userRefreshToken = _tokenService.GenerateRefreshToken(user);

                UserRefreshTokenReadDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(userRefreshToken);

                return new ApiResponse<string, UserRefreshTokenReadDto>(token, userRefreshTokenDto, true, "Registration completed successfully", 200);
            }
            catch (DbUpdateException ex)
            {
                return new ApiResponse<string, UserRefreshTokenReadDto>(null, null,false, $"Erro: {ex.InnerException?.Message}", 500);
            }
        }

        public async Task<ApiResponse<string, UserRefreshTokenReadDto>> RefreshToken(UserRefreshTokenRefreshDto userRefreshTokenRefreshDto)
        {
            try
            {
                UserRefreshToken userRefreshToken = await _userRefreshTokenRepository.ReadUserRefreshByTokenAsync(userRefreshTokenRefreshDto.RefreshToken);
                User user = await _userRepository.ReadUserByIdAsync(userRefreshToken.UserId);

                var newToken = _tokenService.GenerateToken(user, "login", int.Parse(_configuration["JwtSettings:ExpirationMinutesJwt"]));

                UserRefreshTokenReadDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(userRefreshToken);

                return new ApiResponse<string, UserRefreshTokenReadDto>(newToken, userRefreshTokenDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<string, UserRefreshTokenReadDto>(null, null, false, $"{ex.Message}", 500);
            }
        }

        public async Task<ApiResponse> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
        {
            try
            {
                User user = await _userRepository.ReadUserByEmailAsync(forgotPasswordDto.Email);

                if (user == null)
                {
                    return new ApiResponse(false, $"Error: user does not exist", 500);
                }

                string token = _tokenService.GenerateToken(user, "resetPassword", int.Parse(_configuration["JwtSettings:ExpiritaionMinutesResetPassword"]));
                string resetLink = $"{_configuration["FrontEnd:Url"]}/reset-password?token={Uri.EscapeDataString(token)}&email={user.Email}";

                bool sendEmail = await _emailService.SendEmailAsync(
                    user.Email,
                    "Password Reset", 
                    $"Click the link to reset your password: <a href='{resetLink}'>Reset Password</a>");

                if (!sendEmail)
                {
                    return new ApiResponse(false, $"Error: Email not send", 500);
                }

                return new ApiResponse(sendEmail, "Email sent successfully", 200);
            }
            catch(Exception ex)
            {
                return new ApiResponse(false, $"Error: {ex.Message}", 500);
            }

        }

        public async Task<ApiResponse> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            try
            {
                User user = await _userRepository.ReadUserByEmailAsync(resetPasswordDto.Email);
                if (user == null)
                {
                    return new ApiResponse(false, "User not found", 404);
                }
                
                bool isValidate = _tokenService.ValidateToken(resetPasswordDto.Token);
                if (!isValidate)
                {
                    return new ApiResponse(false, "Invalid or expired token", 400);
                }

                resetPasswordDto.NewPassword = _encryptionService.HashPassword(resetPasswordDto.NewPassword);
                //await _userRepository.UpdateUserAsync(user);

                Console.WriteLine(resetPasswordDto.Email);
                Console.WriteLine(resetPasswordDto.Token);
                Console.WriteLine(resetPasswordDto.NewPassword);

                return new ApiResponse(true, "Password reset successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse(false, $"Error: {ex.Message}", 500);
            }
        }
    }
}
