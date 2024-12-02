using ToDoFlow.Domain.Models;

namespace ToDoFlow.Application.Dtos
{
    public class CategoryBaseDto
    {
        public string Name { get; set; }
    }
    public class CategoryCreateDto : CategoryBaseDto { }
    public class CategoryReadDto : CategoryBaseDto
    {
        public List<TaskItem> Tasks { get; set; }
    }
    public class CategoryUpdateDto : CategoryBaseDto { }
}
