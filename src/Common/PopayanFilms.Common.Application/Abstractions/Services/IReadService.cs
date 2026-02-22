using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Application.Abstractions.Services;

public interface IReadService<TEntity, TId, TResponse>
    where TEntity : Entity<TId>
    where TId : notnull
{
    Task<Result<TResponse>> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<TResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
}

public interface IReadService<TEntity, TResponse>
    : IReadService<TEntity, Guid, TResponse>
    where TEntity : Entity<Guid>
{
}
