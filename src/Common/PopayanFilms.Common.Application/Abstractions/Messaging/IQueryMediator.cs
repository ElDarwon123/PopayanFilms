namespace PopayanFilms.Common.Application.Abstractions.Messaging;

public interface IQueryMediator
{
    Task<TResult> QueryAsync<TResult>(IQueryBase<TResult> query, CancellationToken cancellationToken = default);
}
