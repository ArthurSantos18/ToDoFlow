using ToDoFlow.Domain.Models;
using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Application.Dtos
{
    public abstract class UserBaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class UserCreateDto : UserBaseDto
    {
        public string PasswordHash { get; set; }
    }

    public class UserReadDto : UserBaseDto
    {
        public int Id { get; set; }
        public List<CategoryReadDto> Categories { get; set; }
        public Profile? Profile { get; set; }
    }

    public class UserUpdateDto : UserBaseDto
    {
        public int Id { get; set; }
        public Profile? Profile { get; set; }
    }
}
