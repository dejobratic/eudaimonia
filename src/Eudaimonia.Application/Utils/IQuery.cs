namespace Eudaimonia.Application.Utils;

public interface IQuery
{
}

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery
{
    Task<TResult> HandleAsync(TQuery query);
}