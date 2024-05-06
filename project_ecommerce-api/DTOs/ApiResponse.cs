namespace project_ecommerce_api.DTOs
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ApiResponse(T data, string message = "Success")
        {
            Success = true;
            Data = data;
            Message = message;
            Errors = new List<string>();
        }

        public ApiResponse(List<string> errors)
        {
            Success = false;
            Errors = errors ?? new List<string>();
            Data = default;
        }
    }
}
