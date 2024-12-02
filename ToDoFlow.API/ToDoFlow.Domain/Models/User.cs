using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public Profile Profile { get; set; }
        public List<TaskItem> Tasks { get; set; }
    }
}
