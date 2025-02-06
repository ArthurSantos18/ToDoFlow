namespace ToDoFlow.Services.Services.Interface
{
    public interface IEnumService
    {
        public ApiResponse<Dictionary<int, string>> ReadPriorities();
    }
}
