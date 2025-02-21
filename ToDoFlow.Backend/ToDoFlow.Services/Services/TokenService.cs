using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ToDoFlow.Domain.Models;
using ToDoFlow.Services.Services.Interface;

namespace ToDoFlow.Services.Services
{
    public class TokenService(IConfiguration configuration): ITokenService
    {
        private readonly IConfiguration _configuration = configuration;

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

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? string.Empty));
            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["JwtSettings:ExpirationMinutesJwt"])),
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
    }
}
