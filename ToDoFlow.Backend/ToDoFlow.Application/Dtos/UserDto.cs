using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Application.Dtos
{
    public abstract class UserBaseDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
    }

    public class UserCreateDto : UserBaseDto
    {
        public required string Password { get; set; }
    }

    public class UserReadDto : UserBaseDto
    {
        public int Id { get; set; }
        public List<CategoryReadDto>? Categories { get; set; }
        public Profile Profile { get; set; }
    }

    public class UserUpdateDto : UserBaseDto
    {
        public int Id { get; set; }
        public required string Password { get; set; }
        public Profile Profile { get; set; }
    }
}
