using Microsoft.EntityFrameworkCore;
using ToDoFlow.Domain.Models;
using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Infrastructure.Context
{
    public static class SeedingContext
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            User u1 = new User
            {
                Id = 1,
                Name = "Admin",
                Email = "admin@gmail.com",
                Password = "$2a$11$ZVHygbDAmzxjzbIEOLPBluUfUToFaqskwUO4r7YzWQSlJJ9DWwKhq", 
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0),
                Profile = Profile.Administrator,
            };

            UserRefreshToken urt1 = new UserRefreshToken
            {
                Id = 1,
                Expiration = new DateTime(1986, 10, 4, 0, 0, 0),
                RefreshToken = "",
                UserId = u1.Id,
            };

            Category c1 = new Category
            {
                Id = 1,
                Name = "Home",
                UserId = u1.Id,
            };

            TaskItem t1 = new TaskItem
            {
                Id = 1,
                Name = "Wash the dishes",
                Description = "Clean all the dirty dishes, including plates, glasses, and utensils.",
                Priority = Priority.Medium,
                CategoryId = c1.Id,
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0)
            };

            TaskItem t2 = new TaskItem
            {
                Id = 2,
                Name = "Clean the house",
                Description = "Vacuum the floors, mop the surfaces, and tidy up the rooms.",
                Priority = Priority.High,
                CategoryId = c1.Id,
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0)
            };

            Category c2 = new Category
            {
                Id = 2,
                Name = "Shopping",
                UserId = u1.Id,
            };

            TaskItem t3 = new TaskItem
            {
                Id = 3,
                Name = "Grocery shopping",
                Description = "Buy groceries including vegetables, fruits, bread, and milk.",
                Priority = Priority.Critical, 
                CategoryId = c2.Id,
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0)
            };

            TaskItem t4 = new TaskItem
            {
                Id = 4,
                Name = "Electronics shopping",
                Description = "Buy new headphones and a phone charger.",
                Priority = Priority.Low,
                CategoryId = c2.Id,
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0)
            };


            User u2 = new User
            {
                Id = 2,
                Name = "Misumi Uika",
                Email = "doloris@gmail.com",
                Password = "$2a$11$nmsDFSlnFp4QFOZ76qfBOeF4H7AxA2Tc6zASVRw/2..MqPELfZT6C", 
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0),
                Profile = Profile.Default,
            };

            UserRefreshToken urt2 = new UserRefreshToken
            {
                Id = 2,
                Expiration = new DateTime(1986, 10, 4, 0, 0, 0),
                RefreshToken = "",
                UserId = u2.Id,
            };

            Category c3 = new Category
            {
                Id = 3,
                Name = "Work",
                UserId = u2.Id,
            };

            TaskItem t5 = new TaskItem
            {
                Id = 5,
                Name = "Complete project report",
                Description = "Finalize the report for the current project, including graphs and conclusions.",
                Priority = Priority.High,
                CategoryId = c3.Id,
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0)

            };

            TaskItem t6 = new TaskItem
            {
                Id = 6,
                Name = "Attend team meeting",
                Description = "Join the weekly team meeting to discuss project progress and goals.",
                Priority = Priority.Medium,
                CategoryId = c3.Id,
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0)
            };

            TaskItem t7 = new TaskItem
            {
                Id = 7,
                Name = "Check emails",
                Description = "Go through and reply to important work-related emails.",
                Priority = Priority.Low,
                CategoryId = c3.Id,
                CreatedAt = new DateTime(1986, 10, 4, 0, 0, 0)
            };


            modelBuilder.Entity<User>().HasData(u1, u2);
            modelBuilder.Entity<Category>().HasData(c1, c2, c3);
            modelBuilder.Entity<TaskItem>().HasData(t1, t2, t3, t4, t5, t6, t7);
            modelBuilder.Entity<UserRefreshToken>().HasData(urt1, urt2);
            
        }
    }
}
