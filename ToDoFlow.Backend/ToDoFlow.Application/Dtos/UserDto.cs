﻿using System.Text.Json.Serialization;
using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Application.Dtos
{
    public abstract class UserBaseDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public Profile Profile { get; set; }
    }

    public class UserCreateDto : UserBaseDto
    {
        public required string Password { get; set; }
    }

    public class UserReadDto : UserBaseDto
    {
        public int Id { get; set; }
        public int CategoryCount { get; set; }
        public int TaskItemCount { get; set; }

    }

    public class UserUpdateDto : UserBaseDto
    {
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }

}
