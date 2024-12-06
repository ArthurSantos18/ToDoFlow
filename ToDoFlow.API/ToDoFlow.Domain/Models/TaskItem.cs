using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Domain.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; } = Status.Pendente;
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompleteAt { get; set; } = null;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
