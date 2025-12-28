using System.Net;

namespace ECommerce.SharedKernel.Domain;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public string Message { get; }
    public HttpStatusCode StatusCode { get; }
    public IReadOnlyList<ValidationError> ValidationErrors { get; }

    protected Result(
        bool isSuccess,
        string message,
        HttpStatusCode statusCode,
        IReadOnlyList<ValidationError> validationErrors = null)
    {
        IsSuccess = isSuccess;
        Message = message;
        StatusCode = statusCode;
        ValidationErrors = validationErrors ?? Array.Empty<ValidationError>();
    }

    public static Result Success(string message = "Operation completed successfully")
    {
        return new Result(true, message, HttpStatusCode.OK);
    }

    public static Result Created(string message = "Resource created successfully")
    {
        return new Result(true, message, HttpStatusCode.Created);
    }

    public static Result NoContent(string message = "Operation completed")
    {
        return new Result(true, message, HttpStatusCode.NoContent);
    }

    public static Result Failure(
        string message,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Result(false, message, statusCode);
    }

    public static Result NotFound(string message = "Resource not found")
    {
        return new Result(false, message, HttpStatusCode.NotFound);
    }

    public static Result Conflict(string message = "Resource already exists")
    {
        return new Result(false, message, HttpStatusCode.Conflict);
    }

    public static Result ValidationFailure(IReadOnlyList<ValidationError> errors)
    {
        return new Result(false, "Validation failed", HttpStatusCode.BadRequest, errors);
    }

    public static Result Unauthorized(string message = "Unauthorized access")
    {
        return new Result(false, message, HttpStatusCode.Unauthorized);
    }

    public static Result Forbidden(string message = "Access forbidden")
    {
        return new Result(false, message, HttpStatusCode.Forbidden);
    }
}

public class Result<T> : Result
{
    public T Data { get; }

    public Result(
        T data,
        bool isSuccess,
        string message,
        HttpStatusCode statusCode,
        IReadOnlyList<ValidationError> validationErrors = null)
        : base(isSuccess, message, statusCode, validationErrors)
    {
        Data = data;
    }

    public static Result<T> Success(
        T data,
        string message = "Operation completed successfully")
    {
        return new Result<T>(data, true, message, HttpStatusCode.OK);
    }

    public static Result<T> Created(
        T data,
        string message = "Resource created successfully")
    {
        return new Result<T>(data, true, message, HttpStatusCode.Created);
    }

    public static new Result<T> Failure(
        string message,
        HttpStatusCode statusCode = HttpStatusCode.BadRequest)
    {
        return new Result<T>(default, false, message, statusCode);
    }

    public static new Result<T> NotFound(string message = "Resource not found")
    {
        return new Result<T>(default, false, message, HttpStatusCode.NotFound);
    }

    public static new Result<T> Conflict(string message = "Resource already exists")
    {
        return new Result<T>(default, false, message, HttpStatusCode.Conflict);
    }

    public static new Result<T> ValidationFailure(IReadOnlyList<ValidationError> errors)
    {
        return new Result<T>(default, false, "Validation failed", HttpStatusCode.BadRequest, errors);
    }

    public static new Result<T> Unauthorized(string message = "Unauthorized access")
    {
        return new Result<T>(default, false, message, HttpStatusCode.Unauthorized);
    }

    public static new Result<T> Forbidden(string message = "Access forbidden")
    {
        return new Result<T>(default, false, message, HttpStatusCode.Forbidden);
    }
}