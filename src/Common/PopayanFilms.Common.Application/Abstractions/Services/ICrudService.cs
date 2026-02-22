using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Application.Abstractions.Services;

public interface ICrudService<TEntity, TId, TCreateCommand, TUpdateCommand, TResponse>
    where TEntity : Entity<TId>
    where TId : notnull
{
    Task<Result<TResponse>> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<Result<IReadOnlyList<TResponse>>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Result<TId>> CreateAsync(TCreateCommand command, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(TId id, TUpdateCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(TId id, CancellationToken cancellationToken = default);
}

public interface ICrudService<TEntity, TCreateCommand, TUpdateCommand, TResponse>
    : ICrudService<TEntity, Guid, TCreateCommand, TUpdateCommand, TResponse>
    where TEntity : Entity<Guid>
{
}
