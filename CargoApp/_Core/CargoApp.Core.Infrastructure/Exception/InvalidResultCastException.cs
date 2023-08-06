using CargoApp.Core.Infrastructure.Response;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Core.Infrastructure.Exception;

public class InvalidResultCastException : SystemException
{
    public InvalidResultCastException(bool isSuccess) : base(
        $"Cannot cast result object to {(isSuccess ? " error model" : " success model")} if result is {(isSuccess ? "is success" : "is error")}",
        StatusCodes.Status500InternalServerError)
    {
    }

    public static void ThrowIfCantCast<TSuccess, TError>(Result<TSuccess, TError> result, bool castToSuccess)
        where TSuccess : class
        where TError : class
    {
        switch (castToSuccess)
        {
            case true when result.SuccessModel is null:
                throw new InvalidResultCastException(true);
            case false when result.ErrorModel is null:
                throw new InvalidResultCastException(false);
        }
    }
}