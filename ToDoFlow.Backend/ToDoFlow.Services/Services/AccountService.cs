using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services
{
    public class AccountService(IConfiguration configuration, IEncryptionService encryptionService, IUserRepository userRepository, IMapper mapper) : IAccountService
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IEncryptionService _encryptionService = encryptionService;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<ApiResponse<string>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            User user = await _userRepository.ReadUserAsync(loginRequestDto.Email);

            if (user == null) 
            {
                return new ApiResponse<string>(null, false, "Erro: User not found", 404);
            }

            if (!_encryptionService.VerifyPassword(loginRequestDto.Password, user.Password))
            {
                return new ApiResponse<string>(null, false, "Erro: Invalid credentials", 401);
            }

            return new ApiResponse<string>(GenerateToken(user), true, "Login successfully", 200);
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            User existingUser = await _userRepository.ReadUserAsync(registerRequestDto.Email);
            
            if (existingUser != null)
            {
                return new ApiResponse<string>(null, false, "Erro: User already exists", 400);
            }

            if (registerRequestDto.Password != registerRequestDto.ConfirmPassword)
            {
                return new ApiResponse<string>(null, false, "Erro: Passwords do not match", 400);
            }

            try
            {
                User user = _mapper.Map<User>(registerRequestDto);
                user.Password = _encryptionService.HashPassword(user.Password);

                await _userRepository.CreateUserAsync(user);

                return new ApiResponse<string>(GenerateToken(user), true, "Registration completed successfully", 200);
            }
            catch (DbUpdateException ex)
            {
                return new ApiResponse<string>(null, false, $"Erro: {ex.InnerException?.Message}", 500);
            }
        }

        public string GenerateToken(User user)
        {
            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");

            Claim[] claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.Role, user.Profile.ToString())
            ];


            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"] ?? string.Empty));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"] ?? string.Empty)),
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
