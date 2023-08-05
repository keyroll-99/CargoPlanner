namespace CargoApp.Core.Infrastructure.Exception;

public class SystemException : System.Exception
{
    public SystemException(string? message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    public int StatusCode { get; private set; }
}