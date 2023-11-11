namespace Eudaimonia.Application.Utils.Queries;

public interface IQueryDispatcher
{
    Task<TResult> DispatchAsync<TResult>(IQuery query, CancellationToken cancellationToken = default);
}