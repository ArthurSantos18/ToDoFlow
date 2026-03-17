using Bogus;
using FluentAssertions;
using ToDoFlow.Domain.Models;
using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Tests.Domain.Models
{
    [Trait("Category", "TaskItemTests")]
    public class TaskItemTests
    {
        private readonly Faker _faker = new("pt_BR");

        [Fact]
        public void Should_CreateTaskItemSuccessfullyWhenAllParamsIsProvided()
        {
            // Arrange
            int expectedId = _faker.Random.Number(0, 9999);
            string expectedName = _faker.Name.JobTitle();
            string expectedDescription = _faker.Lorem.Paragraph();
            Status expectedStatus = _faker.PickRandom(Status.InProgress, Status.Pending);
            Priority expectedPriority = _faker.PickRandom<Priority>();
            DateTime expectedCreatedAt = DateTime.UtcNow;

            Category expectedCategory = new()
            {
                Id = _faker.Random.Number(0, 9999),
                Name = _faker.Name.JobType() 
            };

            // Act
            TaskItem taskItem = new()
            {
                Id = expectedId,
                Name = expectedName,
                Description = expectedDescription,
                Status = expectedStatus,
                Priority = expectedPriority,
                CreatedAt = expectedCreatedAt,
                Category = expectedCategory,
                CategoryId = expectedCategory.Id
            };

            // Assert
            taskItem.Id.Should().Be(expectedId);
            taskItem.Name.Should().Be(expectedName);
            taskItem.Description.Should().Be(expectedDescription);
            taskItem.Status.Should().Be(expectedStatus);
            taskItem.Priority.Should().Be(expectedPriority);
            taskItem.CreatedAt.Should().Be(expectedCreatedAt);
            taskItem.Category.Should().Be(expectedCategory);
            taskItem.CategoryId.Should().Be(expectedCategory.Id);
        }

        [Fact]
        public void Should_UpdateTaskItemSuccessfullyWhenStatusIsComplete()
        {
            // Arrange
            Category category = new()
            {
                Id = _faker.Random.Number(0, 9999),
                Name = _faker.Name.JobType()
            };

            TaskItem taskItem = new()
            {
                Id = _faker.Random.Number(0, 9999),
                Name = _faker.Name.JobTitle(),
                Description = _faker.Name.JobDescriptor(),
                Status = _faker.PickRandom(Status.InProgress, Status.Pending),
                Priority = _faker.PickRandom<Priority>(),
                Category = category,
                CategoryId = category.Id
            };

            Status expectedStatus = Status.Complete;
            DateTime expectedCompleteAt = DateTime.UtcNow;

            // Act
            taskItem.Status = expectedStatus;
            taskItem.CompleteAt = expectedCompleteAt;

            // Assert
            taskItem.Status.Should().Be(expectedStatus);
            taskItem.CompleteAt.Should().Be(expectedCompleteAt);
        }
    }
}
