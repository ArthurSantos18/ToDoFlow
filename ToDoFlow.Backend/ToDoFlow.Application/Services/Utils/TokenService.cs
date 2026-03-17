using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ToDoFlow.Application.Services.Interfaces;
using ToDoFlow.Domain.Models;
using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Application.Services.Utils
{
    public class TokenService(IConfiguration configuration) : ITokenService
    {
        private readonly IConfiguration _configuration = configuration;

        public string GenerateToken(User user, string purpose)
        {
            int expire_time;

            List<Claim> claims =
            [
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new("purpose", purpose),
            ];

            if (purpose == TokenPurpose.Login.ToString())
            {
                expire_time = int.Parse(_configuration["JwtSettings:ExpirationMinutesJwt"]);
                claims.Add(new Claim(ClaimTypes.Role, user.Profile.ToString()));
            }
            else
            {
                expire_time = int.Parse(_configuration["JwtSettings:ExpirationMinutesResetPassword"]);
            }

                SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? string.Empty));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.UtcNow.AddMinutes(expire_time),
                claims: claims,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserRefreshToken GenerateRefreshToken(User user)
        {
            byte[] randomNumber = new byte[32];

            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            UserRefreshToken userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                RefreshToken = Convert.ToBase64String(randomNumber),
                Expiration = DateTime.UtcNow.AddDays(int.Parse(_configuration["JwtSettings:ExpirationDaysRefresh"]))
            };

            return userRefreshToken;
        }

        public ClaimsPrincipal ValidateToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? string.Empty));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidateAudience = true,
                ValidAudience = _configuration["JwtSettings:Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

            if (principal.FindFirst("purpose")?.Value != TokenPurpose.ResetPassword.ToString())
            {
                throw new ValidationException("Invalid token purpose");
            }

            return principal;    
        }
        
    }
    
}


