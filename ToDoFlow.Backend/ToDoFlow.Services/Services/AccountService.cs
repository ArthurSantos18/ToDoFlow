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

            string token = GenerateToken(user, "login", int.Parse(_jwtSettings["ExpirationMinutesJwt"]));

            await _userRefreshTokenRepository.DeleteExpiredTokensByUserIdAsync(user.Id);

            UserRefreshToken existingRefreshToken = await _userRefreshTokenRepository.ReadUserRefreshByUserIdAsync(user.Id);

            if (existingRefreshToken == null)
            {
                UserRefreshToken userRefreshToken = GenerateRefreshToken(user);
                await _userRefreshTokenRepository.CreateUserRefreshTokenAsync(userRefreshToken);
                UserRefreshTokenReadDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(userRefreshToken);
                
                return new ApiResponse<string, UserRefreshTokenReadDto>(token, userRefreshTokenDto, true, "Login successfully", 200);
            }
            else
            {
                await _userRefreshTokenRepository.DeleteUserRefreshTokenAsync(existingRefreshToken.RefreshToken);
                
                UserRefreshToken userRefreshToken = GenerateRefreshToken(user);
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

                string token = GenerateToken(user, "refresh", int.Parse(_jwtSettings["ExpirationMinutesJwt"]));
                UserRefreshToken userRefreshToken = GenerateRefreshToken(user);

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

                var newToken = GenerateToken(user, "login", int.Parse(_jwtSettings["ExpirationMinutesJwt"]));

                UserRefreshTokenReadDto userRefreshTokenDto = _mapper.Map<UserRefreshTokenReadDto>(userRefreshToken);

                return new ApiResponse<string, UserRefreshTokenReadDto>(newToken, userRefreshTokenDto, true, "Operation carried out successfully", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<string, UserRefreshTokenReadDto>(null, null, false, $"{ex.Message}", 500);
            }
        }

        public string GenerateToken(User user, string purpose, int expiryMinutes)
        {
            List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("purpose", purpose),
            ];

            if (purpose == "login" || purpose == "refresh")
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Profile.ToString()));
                
            }

            foreach (var claim in claims)
            {
                Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
            }

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_jwtSettings["SecretKey"] ?? string.Empty));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _jwtSettings["Issuer"],
                audience: _jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_jwtSettings["ExpirationMinutesJwt"])),
                claims: claims,
                signingCredentials: creds);

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
                    Expiration = DateTime.UtcNow.AddMinutes(int.Parse(_jwtSettings["ExpirationMinutesRefresh"]))
                };

            return userRefreshToken; 
            }
        }
    }
}
