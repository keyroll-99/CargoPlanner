namespace CargoApp.Core.Infrastructure.Exception;

public abstract class SystemException : System.Exception
{
    public int StatusCode { get; private set; }

    protected SystemException(string? message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
    
    
}