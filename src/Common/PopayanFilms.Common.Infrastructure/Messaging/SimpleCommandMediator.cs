using Microsoft.Extensions.DependencyInjection;
using PopayanFilms.Common.Application.Abstractions.Messaging;

namespace PopayanFilms.Common.Infrastructure.Messaging;

public sealed class SimpleCommandMediator : ICommandMediator
{
    private readonly IServiceProvider _serviceProvider;

    public SimpleCommandMediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResult> SendAsync<TResult>(ICommandBase<TResult> command, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<,>)
            .MakeGenericType(command.GetType(), typeof(TResult));

        dynamic handler = _serviceProvider.GetRequiredService(handlerType);
        return handler.HandleAsync((dynamic)command, cancellationToken);
    }
}
