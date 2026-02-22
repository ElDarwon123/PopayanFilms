using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Application.Abstractions.Messaging;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQueryBase<TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
}
