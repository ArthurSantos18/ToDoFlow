using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Repositories;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services
{
    public class AccountService : IAccountService
    {
        
        private readonly IConfiguration _configuration;
        private readonly IConfigurationSection _jwtSettings;

        private readonly IEncryptionService _encryptionService;
        private readonly IUserRepository _userRepository;
        private readonly IUserRefreshTokenRepository _userRefreshTokenRepository;

        private readonly IMapper _mapper;

        public AccountService(IConfiguration configuration, IEncryptionService encryptionService, IUserRepository userRepository, IMapper mapper, IUserRefreshTokenRepository userRefreshTokenRepository)
        {
            _configuration = configuration;
            _jwtSettings = _configuration.GetSection("JwtSettings");

            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _userRefreshTokenRepository = userRefreshTokenRepository;
            _mapper = mapper;
            
        }

        public async Task<ApiResponse<string, UserRefreshTokenDto>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            User user = await _userRepository.ReadUserByEmailAsync(loginRequestDto.Email);

            if (user == null)
            {
                return new ApiResponse<string, UserRefreshTokenDto>(null, null, false, "Erro: User not found", 404);
            }

            if (!_encryptionService.VerifyPassword(loginRequestDto.Password, user.Password))
            {
                return new ApiResponse<string, UserRefreshTokenDto>(null, null,false, "Erro: Invalid credentials", 401);
            }

            string token = GenerateToken(user, "login", int.Parse(_jwtSettings["ExpirationMinutesLogin"]));

            await _userRefreshTokenRepository.DeleteExpiredTokensByUserIdAsync(user.Id);

            UserRefreshToken existingRefreshToken = await _userRefreshTokenRepository.ReadUserRefreshByUserIdAsync(user.Id);

            if (existingRefreshToken == null)
            {
                UserRefreshToken userRefreshToken = GenerateRefreshToken(user);
                await _userRefreshTokenRepository.CreateUserRefreshTokenAsync(userRefreshToken);
                UserRefreshTokenDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenDto>(userRefreshToken);
                
                return new ApiResponse<string, UserRefreshTokenDto>(token, userRefreshTokenDto, true, "Login successfully", 200);
            }
            else
            {
                await _userRefreshTokenRepository.DeleteUserRefreshTokenAsync(existingRefreshToken.RefreshToken);

                UserRefreshTokenDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenDto>(existingRefreshToken);
                return new ApiResponse<string, UserRefreshTokenDto>(token, userRefreshTokenDto, true, "Login successfully", 200);
            }
    
        }

        public async Task<ApiResponse<string, UserRefreshTokenDto>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            User existingUser = await _userRepository.ReadUserByEmailAsync(registerRequestDto.Email);

            if (existingUser != null)
            {
                return new ApiResponse<string, UserRefreshTokenDto>(null, null, false, "Erro: User already exists", 400);
            }

            if (registerRequestDto.Password != registerRequestDto.ConfirmPassword)
            {
                return new ApiResponse<string, UserRefreshTokenDto>(null, null, false, "Erro: Passwords do not match", 400);
            }

            try
            {
                User user = _mapper.Map<User>(registerRequestDto);
                user.Password = _encryptionService.HashPassword(user.Password);

                await _userRepository.CreateUserAsync(user);

                string token = GenerateToken(user, "refresh", int.Parse(_jwtSettings["ExpirationMinutesLogin"]));
                UserRefreshToken userRefreshToken = GenerateRefreshToken(user);

                UserRefreshTokenDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenDto>(userRefreshToken);

                return new ApiResponse<string, UserRefreshTokenDto>(token, userRefreshTokenDto, true, "Registration completed successfully", 200);
            }
            catch (DbUpdateException ex)
            {
                return new ApiResponse<string, UserRefreshTokenDto>(null, null,false, $"Erro: {ex.InnerException?.Message}", 500);
            }
        }

        public async Task<ApiResponse<string, UserRefreshTokenDto>> RefreshToken(string refreshToken)
        {
            try
            {
                UserRefreshToken userRefreshToken = await _userRefreshTokenRepository.ReadUserRefreshByTokenAsync(refreshToken);
                User user = await _userRepository.ReadUserByIdAsync(userRefreshToken.UserId);

                var newToken = GenerateToken(user, "login", int.Parse(_jwtSettings["ExpirationMinutesLogin"]));

                UserRefreshTokenDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenDto>(userRefreshToken);

                return new ApiResponse<string, UserRefreshTokenDto>(newToken, userRefreshTokenDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<string, UserRefreshTokenDto>(null, null, false, $"{ex.Message}", 500);
            }
        }

        public string GenerateToken(User user, string purpose, int expiryMinutes)
        {
            Claim[] claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("purpose", purpose),
            ];

            if (purpose == "login" || purpose == "refresh")
            {
                claims.Append(new Claim(ClaimTypes.Role, user.Profile.ToString()));
            }

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtSettings["SecretKey"] ?? string.Empty));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtSettings["ExpirationMinutesLogin"])),
                claims: claims,
                signingCredentials: creds);

            Console.WriteLine($"Horário atual: "+ DateTime.Now);
            Console.WriteLine($"Token expira em: {token.ValidTo.ToLocalTime():HH:mm:ss}");



            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserRefreshToken GenerateRefreshToken(User user)
        {
            byte[] randomNumber = new byte[32];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                UserRefreshToken userRefreshToken = new UserRefreshToken
                {
                    UserId = user.Id,
                    RefreshToken = Convert.ToBase64String(randomNumber),
                    Expiration = DateTime.UtcNow.AddMinutes(4)
                };

            return new UserRefreshToken { UserId = user.Id, RefreshToken = Convert.ToBase64String(randomNumber), Expiration = DateTime.UtcNow.AddMinutes(10) }; 
            }
        }
    }
}
