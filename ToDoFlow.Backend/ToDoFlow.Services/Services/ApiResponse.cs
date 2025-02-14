namespace ToDoFlow.Services.Services
{
    public class ApiResponse<T>(T? data, bool success, string message, int httpStatus)
    {
        public T? Data { get; set; } = data;
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public int HttpStatus { get; set; } = httpStatus;
    }

    public class ApiResponse<T1, T2>(T1? data1, T2? data2, bool success, string message, int httpStatus)
    {
        public T1? Data1 { get; set; } = data1;
        public T2? Data2 { get; set; } = data2;
        public bool Success { get; set; } = success;
        public string Message { get; set; } = message;
        public int HttpStatus { get; set; } = httpStatus;
    }
}
