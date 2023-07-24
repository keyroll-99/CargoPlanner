using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Core.Infrastructure.Response;

public class Result<TSuccess, TError>
    where TSuccess : class
    where TError : class
{
    private Result(bool isSuccess, int statusCode, TSuccess? successModel, TError? errorModel)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        SuccessModel = successModel;
        ErrorModel = errorModel;
    }

    public bool IsSuccess { get; }
    public int StatusCode { get; }
    public TSuccess? SuccessModel { get; }
    public TError? ErrorModel { get; }

    public static Result<TSuccess, TError> Fail(TError error, int statusCode)
    {
        return new Result<TSuccess, TError>(false, statusCode, default, error);
    }

    public static Result<TSuccess, TError> Success(TSuccess success, int statusCode)
    {
        return new Result<TSuccess, TError>(true, statusCode, success, default);
    }

    public static implicit operator Result<TSuccess, TError>(TError error)
    {
        return new Result<TSuccess, TError>(false, StatusCodes.Status400BadRequest, default, error);
    }

    public static implicit operator Result<TSuccess, TError>(TSuccess success)
    {
        return new Result<TSuccess, TError>(true, StatusCodes.Status200OK, success, default);
    }

    public ObjectResult GetObjectResult()
    {
        var objectResult = new ObjectResult(IsSuccess ? SuccessModel : ErrorModel)
        {
            StatusCode = StatusCode
        };
        return objectResult;
    }
}