using Bogus;
using FluentAssertions;
using System.Security.Cryptography;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Tests.Domain.Models
{
    [Trait("Category", "UserRefreshTokenTests")]
    public class UserRefreshTokenTests
    {
        private readonly Faker _faker = new("pt_BR");


        [Fact]
        public void Should_CreateCategorySuccessfullyWhenAllParamsIsProvided()
        {
            // Arrange
            int expectedId = _faker.Random.Number(0, 9999);
            DateTime expectedExpiration = _faker.Date.Future();
            User expectedUser = new()
            {
                Id = _faker.Random.Number(0, 9999),
                Name = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password()
            };

            byte[] randomNumber = new byte[32];

            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            string expectedRefreshToken = Convert.ToBase64String(randomNumber);

            // Act
            UserRefreshToken userRefreshToken = new()
            {
                Id = expectedId,
                RefreshToken = expectedRefreshToken,
                Expiration = expectedExpiration,
                User = expectedUser,
                UserId = expectedUser.Id
            };

            // Assert
            userRefreshToken.Id.Should().Be(expectedId);
            userRefreshToken.RefreshToken.Should().Be(expectedRefreshToken);
            userRefreshToken.Expiration.Should().Be(expectedExpiration);
            userRefreshToken.User.Should().Be(expectedUser);
            userRefreshToken.UserId.Should().Be(expectedUser.Id);
        }
    }
}
