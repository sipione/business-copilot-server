
namespace APP.Exceptions;
public class CommonExceptions : Exception{
    public int StatusCode { get; }

    public CommonExceptions(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public static Exception Unauthorized(string message){
        throw new CommonExceptions(message, 401); // 401 is the status code for Unauthorized
    }

    public static Exception Forbidden(string message){
        throw new CommonExceptions(message, 403); // 403 is the status code for Forbidden
    }

    public static Exception InternalServerError(string message){
        throw new CommonExceptions(message, 500); // 500 is the status code for Internal Server Error
    }

    public static Exception NotFound(string message){
        throw new CommonExceptions(message, 404); // 404 is the status code for Not Found
    }

    public static Exception BadRequest(string message){
        throw new CommonExceptions(message, 400); // 400 is the status code for Bad Request
    }

    public static Exception Conflict(string message){
        throw new CommonExceptions(message, 409); // 409 is the status code for Conflict
    }
}