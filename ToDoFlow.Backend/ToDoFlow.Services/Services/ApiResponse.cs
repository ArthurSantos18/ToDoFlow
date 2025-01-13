namespace ToDoFlow.Services.Services
{
    public class ApiResponse<T>(T? data, bool success, string message, int httpStatus)
    {
        public T? Data { get; set; } = data;
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public int HttpStatus { get; set; } = httpStatus;
    }
}
