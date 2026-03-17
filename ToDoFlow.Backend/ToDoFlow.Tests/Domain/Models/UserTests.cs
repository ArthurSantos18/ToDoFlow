using Bogus;
using FluentAssertions;
using NSubstitute;
using ToDoFlow.Application.Services.Interfaces;
using ToDoFlow.Domain.Models;
using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Tests.Domain.Models
{
    [Trait("Category", "UserTests")]
    public class UserTests
    {
        private readonly Faker _faker = new("pt_BR");
        private readonly ITokenService _mockTokenService;

        public UserTests()
        {
            _mockTokenService = Substitute.For<ITokenService>();
        }

        [Fact]
        public void Should_CreateUserSuccessfullyWhenAllParamsIsProvided()
        {
            // Arrange
            int expectedId = _faker.Random.Number(0, 9999);
            string expectedName = _faker.Internet.UserName();
            string expectedEmail = _faker.Internet.Email();
            string expectedPassword = _faker.Internet.Password();
            DateTime expectedCreatedAt = DateTime.UtcNow;
            Profile expectedProfile = _faker.PickRandom<Profile>();
            
            Category category_1 = new() { Name = _faker.Name.JobType() };
            Category category_2 = new() { Name = _faker.Name.JobType() };
            Category category_3 = new() { Name = _faker.Name.JobType() };

            List<Category> categories = [category_1, category_2, category_3];

            // Act
            User user = new()
            {
                Id = expectedId,
                Name = expectedName,
                Email = expectedEmail,
                Password = expectedPassword,
                CreatedAt = expectedCreatedAt,
                Profile = expectedProfile,
                Categories = categories,
            };

            UserRefreshToken expectedUserRefreshToken = _mockTokenService.GenerateRefreshToken(user);

            user.UserRefreshToken = expectedUserRefreshToken;

            // Assert
            user.Id.Should().Be(expectedId);
            user.Name.Should().Be(expectedName);
            user.Email.Should().Be(expectedEmail);
            user.Password.Should().Be(expectedPassword);
            user.CreatedAt.Should().Be(expectedCreatedAt);
            user.Profile.Should().Be(expectedProfile);
            user.UserRefreshToken.Should().Be(expectedUserRefreshToken);
            user.Categories.Should().Equal(categories);
            
        }
    }
}
