﻿namespace Eudaimonia.Application.Utils.Queries;

public class QueryDispatcher : IQueryDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public QueryDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResult> DispatchAsync<TResult>(IQuery query)
    {
        var (handler, handlerType) = ResolveQueryHandler<TResult>(query);

        var handleMethod = handlerType.GetMethod("HandleAsync");
        return await (Task<TResult>)handleMethod!.Invoke(handler, new object[] { query })!;
    }

    private (object, Type) ResolveQueryHandler<TResult>(IQuery query)
    {
        var queryType = query.GetType();
        var queryHandlerType = typeof(IQueryHandler<,>).MakeGenericType(queryType, typeof(TResult));

        var queryHandler = _serviceProvider.GetService(queryHandlerType)
            ?? throw new InvalidOperationException($"No query handler found for query type {queryType.Name}.");

        return (queryHandler, queryHandlerType);
    }
}