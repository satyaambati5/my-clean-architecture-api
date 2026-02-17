namespace MyProject.Common.Exceptions
{
    public abstract class CustomException : Exception
    {
        public int StatusCode { get; }
        public List<string> Errors { get; }

        protected CustomException(string message, int statusCode = 400, List<string> errors = null)
            : base(message)
        {
            StatusCode = statusCode;
            Errors = errors ?? new List<string>();
        }
    }

    public class NotFoundException : CustomException
    {
        public NotFoundException(string message = "Resource not found")
            : base(message, 404)
        {
        }

        public NotFoundException(string resourceName, object key)
            : base($"{resourceName} with id '{key}' was not found", 404)
        {
        }
    }

    public class BadRequestException : CustomException
    {
        public BadRequestException(string message = "Bad request")
            : base(message, 400)
        {
        }

        public BadRequestException(List<string> errors)
            : base("Validation failed", 400, errors)
        {
        }
    }

    public class ValidationException : CustomException
    {
        public ValidationException(List<string> errors)
            : base("One or more validation errors occurred", 422, errors)
        {
        }

        public ValidationException(string message)
            : base(message, 422)
        {
        }
    }

    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message = "Unauthorized access")
            : base(message, 401)
        {
        }
    }

    public class ForbiddenException : CustomException
    {
        public ForbiddenException(string message = "Access forbidden")
            : base(message, 403)
        {
        }
    }

    public class ConflictException : CustomException
    {
        public ConflictException(string message = "Resource conflict")
            : base(message, 409)
        {
        }
    }

    public class BusinessException : CustomException
    {
        public BusinessException(string message)
            : base(message, 400)
        {
        }
    }
}