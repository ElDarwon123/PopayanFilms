using Microsoft.Extensions.DependencyInjection;
using PopayanFilms.Common.Application.Abstractions.Messaging;

namespace PopayanFilms.Common.Infrastructure.Messaging;

public sealed class SimpleQueryMediator : IQueryMediator
{
    private readonly IServiceProvider _serviceProvider;

    public SimpleQueryMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResult> QueryAsync<TResult>(IQueryBase<TResult> query, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IQueryHandler<,>)
            .MakeGenericType(query.GetType(), typeof(TResult));

        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        return handler.HandleAsync((dynamic)query, cancellationToken);
    }
}
