namespace MyProject.Common.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; }

        public ApiResponse()
        {
            Timestamp = DateTime.UtcNow;
            Errors = new List<string>();
        }

        public ApiResponse(T data, string message = null)
        {
            Success = true;
            Data = data;
            Message = message ?? "Request successful";
            StatusCode = 200;
            Timestamp = DateTime.UtcNow;
            Errors = new List<string>();
        }

        public ApiResponse(string message, int statusCode = 400)
        {
            Success = false;
            Message = message;
            StatusCode = statusCode;
            Timestamp = DateTime.UtcNow;
            Errors = new List<string>();
        }

        public ApiResponse(List<string> errors, string message = "Request failed", int statusCode = 400)
        {
            Success = false;
            Message = message;
            Errors = errors ?? new List<string>();
            StatusCode = statusCode;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class ApiResponse : ApiResponse<object>
    {
        public ApiResponse(string message, bool success = true, int statusCode = 200)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
            Timestamp = DateTime.UtcNow;
            Errors = new List<string>();
        }
    }
}