using CargoApp.Core.Infrastructure.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Core.Infrastructure.Response;

public class Result<TSuccess, TError>
    where TSuccess : class
    where TError : class
{
    protected Result(bool isSuccess, int statusCode, TSuccess? successModel, TError? errorModel)
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

    public static Result<TSuccess, TError> Fail(TError error, int statusCode = StatusCodes.Status400BadRequest)
    {
        return new Result<TSuccess, TError>(false, statusCode, default, error);
    }

    public static Result<TSuccess, TError> Success(TSuccess success, int statusCode = StatusCodes.Status200OK)
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

    public static implicit operator TSuccess(Result<TSuccess, TError> result)
    {
        InvalidResultCastException.ThrowIfCantCast(result, true);
        return result.SuccessModel!;
    }

    public static implicit operator TError(Result<TSuccess, TError> result)
    {
        InvalidResultCastException.ThrowIfCantCast(result, false);
        return result.ErrorModel!;
    }

    public (TSuccess?, TError?) Match(Func<TSuccess, TSuccess> onSuccess, Func<TError, TError> onError)
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }

        return (null, onError(ErrorModel!));
    }
    
    public (TSuccessResult?, TErrorResult?) Match<TSuccessResult, TErrorResult>(Func<TSuccess, TSuccessResult> onSuccess, Func<TError, TErrorResult> onError)
        where TSuccessResult : class
        where TErrorResult : class
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }

        return (null, onError(ErrorModel!));
    }
    
    public (TSuccessResult?, TError?) MatchOnlySuccess<TSuccessResult>(Func<TSuccess, TSuccessResult> onSuccess)
        where TSuccessResult : class
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }

        return (null, ErrorModel);
    }
    
    public (TSuccess?, TError?) MatchOnlySuccess(Func<TSuccess, TSuccess> onSuccess)
    {
        if (IsSuccess)
        {
            return (onSuccess(SuccessModel!), null);
        }

        return (null, ErrorModel);
    }
    
    public (TSuccess?, TErrorResult?) MatchOnlyError<TErrorResult>(Func<TError, TErrorResult> onError)
        where TErrorResult : class
    {
        if (IsSuccess)
        {
            return (SuccessModel, null);
        }

        return (null, onError(ErrorModel!));
    }
    
    public (TSuccess?, TError?) MatchOnlyError(Func<TError, TError> onError)
    {
        if (IsSuccess)
        {
            return (SuccessModel, null);
        }

        return (null, onError(ErrorModel!));
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

public class Result<TSuccess> : Result<TSuccess, string>
    where TSuccess : class
{
    public Result(bool isSuccess, int statusCode, TSuccess? successModel, string? errorModel) : base(isSuccess, statusCode, successModel, errorModel)
    {
    }
    
    public new static Result<TSuccess> Fail(string error, int statusCode = StatusCodes.Status400BadRequest)
    {
        return new Result<TSuccess>(false, statusCode, default, error);
    }

    public new static Result<TSuccess> Success(TSuccess success, int statusCode = StatusCodes.Status200OK)
    {
        return new Result<TSuccess>(true, statusCode, success, default);
    }

    public static implicit operator Result<TSuccess>(string error)
    {
        return new Result<TSuccess>(false, StatusCodes.Status400BadRequest, default, error);
    }

    public static implicit operator Result<TSuccess>(TSuccess success)
    {
        return new Result<TSuccess>(true, StatusCodes.Status200OK, success, default);
    }
}