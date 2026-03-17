using Bogus;
using FluentAssertions;
using ToDoFlow.Domain.Models;

namespace ToDoFlow.Tests.Domain.Models
{
    [Trait("Category", "CategoryTests")]
    public class CategoryTests()
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact]
        public void Should_CreateCategorySuccessfullyWhenAllParamsIsProvided()
        {
            // Arrange
            int expectedId = _faker.Random.Number(0, 9999);
            string expectedCategoryName = _faker.Name.JobType();
            User expectedUser = new()
            {
                Id = _faker.Random.Number(0, 9999),
                Name = _faker.Internet.UserName(),
                Email = _faker.Internet.Email(),
                Password = _faker.Internet.Password()
            };

            TaskItem taskItem1 = new() { Name = _faker.Name.JobTitle(), Description = _faker.Lorem.Paragraph() };
            TaskItem taskItem2 = new() { Name = _faker.Name.JobTitle(), Description = _faker.Lorem.Paragraph() };
            TaskItem taskItem3 = new() { Name = _faker.Name.JobTitle(), Description = _faker.Lorem.Paragraph() };
            List<TaskItem> expectedTasks = [taskItem1, taskItem2, taskItem3];

            // Act
            Category category = new()
            {
                Id = expectedId,
                Name = expectedCategoryName,
                UserId = expectedUser.Id,
                User = expectedUser,
                Tasks = expectedTasks
            };

            // Assert
            category.Id.Should().Be(expectedId);
            category.Name.Should().Be(expectedCategoryName);
            category.UserId.Should().Be(expectedUser.Id);
            category.User.Should().Be(expectedUser);
            category.Tasks.Should().Equal(expectedTasks);
        }
    }
}
