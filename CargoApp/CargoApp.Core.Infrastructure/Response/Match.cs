using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Core.Infrastructure.Response;

public class Match<TSuccess, TError>
    where TSuccess : class
    where TError : class
{
    public bool IsSuccess { get; private set; }
    public int StatusCode { get; private set; }
    public TSuccess? SuccessModel { get; private set; }
    public TError? ErrorModel { get; private set; }

    private Match(bool isSuccess, int statusCode, TSuccess? successModel, TError? errorModel)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        SuccessModel = successModel;
        ErrorModel = errorModel;
    }

    public static Match<TSuccess, TError> Fail(TError error, int statusCode) => new(false, statusCode, default, error);

    public static Match<TSuccess, TError> Success(TSuccess success, int statusCode) => new(true, statusCode, success, default);

    public static implicit operator Match<TSuccess, TError>(TError error)
        => new(false, StatusCodes.Status400BadRequest, default, error);

    public static implicit operator Match<TSuccess, TError>(TSuccess success)
        => new(true, StatusCodes.Status200OK, success, default);

    public ObjectResult GetObjectResult()
    {
        var objectResult = new ObjectResult(IsSuccess ? SuccessModel : ErrorModel)
        {
            StatusCode = StatusCode
        };
        return objectResult;
    }
}