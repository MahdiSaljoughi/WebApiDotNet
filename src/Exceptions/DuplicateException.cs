namespace WebApi.Exceptions;

public class DuplicateException : BaseException
{
    public DuplicateException(string message) : base(message, 409)
    {
    }
}