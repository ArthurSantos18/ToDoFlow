using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public Profile Profile { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
