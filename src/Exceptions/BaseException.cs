namespace WebApi.Exceptions;

public abstract class BaseException : Exception
{
    public int StatusCode { get; set; }

    protected BaseException(string message, int statusCode = 500) : base(message)
    {
        StatusCode = statusCode;
    }
}