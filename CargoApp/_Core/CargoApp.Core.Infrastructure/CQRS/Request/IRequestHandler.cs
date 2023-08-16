using CargoApp.Core.Infrastructure.Response;

namespace CargoApp.Core.Infrastructure.CQRS.Request;

// TODO: in the future, I want to create generic inject
public interface IRequestHandler<in TRequest, TResponse, TError> 
    where TRequest : IRequest
    where TResponse : class 
    where TError : class
{
    public Task<Result<TResponse, TError>> Handle(TRequest command);
}