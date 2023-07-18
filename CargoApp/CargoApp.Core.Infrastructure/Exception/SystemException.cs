using System.Net;

namespace CargoApp.Core.Infrastructure.Exception;

public class SystemException : System.Exception
{
    public int StatusCode { get; private set; }

    public SystemException(string? message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}