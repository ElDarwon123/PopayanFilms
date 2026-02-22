namespace PopayanFilms.Common.Application.Abstractions.Messaging;

public interface ICommandMediator
{
    Task<TResult> SendAsync<TResult>(ICommandBase<TResult> command, CancellationToken cancellationToken = default);
}
