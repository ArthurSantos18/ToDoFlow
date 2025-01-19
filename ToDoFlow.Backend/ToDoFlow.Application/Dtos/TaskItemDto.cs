using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Application.Dtos
{
    public abstract class TaskItemBaseDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public Priority Priority { get; set; }
    }

    public class TaskItemCreateDto : TaskItemBaseDto
    {
        public int CategoryId { get; set; }
    }

    public class TaskItemReadDto : TaskItemBaseDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CompleteAt { get; set; }
        public Status Status { get; set; }
    }

    public class TaskItemUpdateDto : TaskItemBaseDto    
    {
        public int Id { get; set; }
        public Status? Status { get; set; }
    }
}
