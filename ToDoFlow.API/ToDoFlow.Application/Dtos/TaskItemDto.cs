using ToDoFlow.Domain.Models.Enums;

namespace ToDoFlow.Application.Dtos
{
    public abstract class TaskItemBaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
    }

    public class TaskItemCreateDto : TaskItemBaseDto { }

    public class TaskItemReadDto : TaskItemBaseDto
    {
        public int Id { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? CompleteAt { get; set; } = new DateTime();
        public Status Status { get; set; }
    }

    public class TaskItemUpdateDto : TaskItemBaseDto
    {
        public Status? Status { get; set; }
    }
}
