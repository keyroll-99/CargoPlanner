using CargoApp.Core.Infrastructure.Response;

namespace CargoApp.Core.Infrastructure.CQRS.Query;

public interface IQueryHandler<in TQuery, TResult, TError>
where TQuery : IQuery
where TResult : class
where TError : class
{
    public Task<Result<TResult, TError>> Handle(TQuery query);
}