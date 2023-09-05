using CargoApp.Core.Infrastructure.Exception;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Core.Infrastructure.Response;

public class Result
{
    public bool IsSuccess { get; }
    public int StatusCode { get; }
    public string? Error { get; }

    protected Result(bool isSuccess, int statusCode)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
    }

    private Result(bool isSuccess, int statusCode, string? error)
    {
        IsSuccess = isSuccess;
        StatusCode = statusCode;
        Error = error;
    }

    public static implicit operator Result(string error) => new Result(false, StatusCodes.Status400BadRequest, error);
    public static implicit operator ObjectResult(Result result) => result.GetObjectResult();

    public static Result Success(int statusCode = StatusCodes.Status200OK) => new Result(true, statusCode);

    public static Result Fail(string error, int statusCode = StatusCodes.Status400BadRequest) =>
        new Result(false, statusCode, error);
    
    
    public async Task<Result> Match(Func<Task> onSuccess, Func<string, Task<Result>> onError)
    {
        if (IsSuccess)
        {
            await onSuccess();
            return this;
        }

        return await onError(Error);
    }
    
    public async Task<TResult> Match<TResult>(Func<Task<TResult>> onSuccess, Func<string, Task<TResult>> onError)
    {
        if (IsSuccess)
        {
            return await onSuccess();
        }

        return await onError(Error);
    }
    
    public async Task<(TSuccessResult?, TErrorResult?, bool)> Match<TSuccessResult, TErrorResult>(Func<Task<TSuccessResult>> onSuccess, Func<string, Task<TErrorResult>> onError)
    where TSuccessResult: class
    where TErrorResult: class
    {
        if (IsSuccess)
        {
            return (await onSuccess(), null, true);
        }

        return (null, await onError(Error), false);
    }
    
    public ObjectResult GetObjectResult()
    {
        var objectResult = new ObjectResult(IsSuccess ? null : Error)
        {
            StatusCode = StatusCode
        };
        return objectResult;
    }
}

public class Result<TSuccess, TError> : Result
    where TSuccess : class
    where TError : class
{
    public TSuccess? SuccessModel { get; }
    public TError? ErrorModel { get; }

    protected Result(bool isSuccess, int statusCode, TSuccess? successModel, TError? errorModel) : base(isSuccess,
        statusCode)
    {
        SuccessModel = successModel;
        ErrorModel = errorModel;
    }

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

    public static implicit operator ObjectResult(Result<TSuccess, TError> result) => result.GetObjectResult();

    public static implicit operator TError(Result<TSuccess, TError> result)
    {
        InvalidResultCastException.ThrowIfCantCast(result, false);
        return result.ErrorModel!;
    }

    public async Task<(TSuccess?, TError?)> Match(Func<TSuccess, Task<TSuccess>> onSuccess, Func<TError, Task<TError>> onError)
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
        }

        return (null, await onError(ErrorModel!));
    }

    public async Task<(TSuccessResult?, TErrorResult?)> Match<TSuccessResult, TErrorResult>(
        Func<TSuccess, Task<TSuccessResult>> onSuccess, Func<TError, Task<TErrorResult>> onError)
        where TSuccessResult : class
        where TErrorResult : class
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
        }

        return (null, await onError(ErrorModel!));
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
    public async Task<(TSuccessResult?, TError?)> MatchOnlySuccessAsync<TSuccessResult>(Func<TSuccess, Task<TSuccessResult>> onSuccess)
        where TSuccessResult : class
    {
        if (IsSuccess)
        {
            return (await onSuccess(SuccessModel!), null);
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
    public Result(bool isSuccess, int statusCode, TSuccess? successModel, string? errorModel) : base(isSuccess,
        statusCode, successModel, errorModel)
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