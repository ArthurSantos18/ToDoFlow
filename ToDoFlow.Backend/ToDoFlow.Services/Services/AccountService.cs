using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoFlow.Application.Dtos;
using ToDoFlow.Domain.Models;
using ToDoFlow.Infrastructure.Repositories.Interface;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly IEncryptionService _encryptionService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountService(IConfiguration configuration, IEncryptionService encryptionService, IUserRepository userRepository, IMapper mapper)
        {
            _configuration = configuration;
            _encryptionService = encryptionService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<string>> LoginAsync(LoginRequestDto loginRequestDto)
        {
            User user = await _userRepository.ReadUserAsync(loginRequestDto.Email);

            if (user == null) 
            {
                return new ApiResponse<string>(null, false, "Erro: Usuário não encontrado", 404);
            }

            if (!_encryptionService.VerifyPassword(loginRequestDto.Password, user.Password))
            {
                return new ApiResponse<string>(null, false, "Erro: Credenciais inválidas", 401);
            }

            return new ApiResponse<string>(GenerateToken(user), true, "Login efetuado com sucesso", 200);
        }

        public async Task<ApiResponse<string>> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            User existingUser = await _userRepository.ReadUserAsync(registerRequestDto.Email);
            
            if (existingUser != null)
            {
                return new ApiResponse<string>(null, false, "Erro: Usuário já existe", 400);
            }

            try
            {
                User user = _mapper.Map<User>(registerRequestDto);
                user.Password = _encryptionService.HashPassword(user.Password);

                await _userRepository.CreateUserAsync(user);

                return new ApiResponse<string>(GenerateToken(user), true, "Registro efetuado com sucesso", 200);
            }
            catch (Exception ex)
            {
                return new ApiResponse<string>(null, false, $"Erro: {ex.Message}", 500);
            }
        }

        public string GenerateToken(User user)
        {
            IConfigurationSection jwtSettings = _configuration.GetSection("JwtSettings");

            Claim[] claims =
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim (ClaimTypes.Role, user.Profile.ToString())
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSettings["ExpirationMinutes"])),
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
