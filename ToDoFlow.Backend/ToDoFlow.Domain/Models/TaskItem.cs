using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Domain.Models
{
    public class TaskItem
    {
        public int Id { get; set; }
        public required string Name { get; set; } 
        public required string Description { get; set; } 
        public Status Status { get; set; } = Status.InProgress;
        public Priority Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now.ToLocalTime();
        public DateTime? CompleteAt { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
