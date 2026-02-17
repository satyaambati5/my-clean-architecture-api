namespace MyProject.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; protected set; }
        public bool IsFailure => !IsSuccess;
        public string Message { get; protected set; }
        public List<string> Errors { get; protected set; }

        protected Result(bool isSuccess, string message, List<string> errors = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Errors = errors ?? new List<string>();
        }

        public static Result Success(string message = "Operation successful")
        {
            return new Result(true, message);
        }

        public static Result Failure(string message, List<string> errors = null)
        {
            return new Result(false, message, errors);
        }

        public static Result<T> Success<T>(T data, string message = "Operation successful")
        {
            return new Result<T>(data, true, message);
        }

        public static Result<T> Failure<T>(string message, List<string> errors = null)
        {
            return new Result<T>(default, false, message, errors);
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; private set; }

        internal Result(T data, bool isSuccess, string message, List<string> errors = null)
            : base(isSuccess, message, errors)
        {
            Data = data;
        }
    }
}