namespace ToDoFlow.Application.Dtos
{
    public abstract class CategoryBaseDto
    {
        public required string Name { get; set; }
    }
    public class CategoryCreateDto : CategoryBaseDto
    {
        public int UserId { get; set; }
    }
    public class CategoryReadDto : CategoryBaseDto
    {
        public int Id { get; set; }
        public List<TaskItemReadDto>? Tasks { get; set; }
    }
    public class CategoryUpdateDto : CategoryBaseDto
    {
        public int Id { get; set; }
    }
}
