using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Application.Abstractions.Services;

public interface IWriteService<TEntity, TId, TCreateCommand, TUpdateCommand>
    where TEntity : Entity<TId>
    where TId : notnull
{
    Task<Result<TId>> CreateAsync(TCreateCommand command, CancellationToken cancellationToken = default);
    Task<Result> UpdateAsync(TId id, TUpdateCommand command, CancellationToken cancellationToken = default);
    Task<Result> DeleteAsync(TId id, CancellationToken cancellationToken = default);
}

public interface IWriteService<TEntity, TCreateCommand, TUpdateCommand>
    : IWriteService<TEntity, Guid, TCreateCommand, TUpdateCommand>
    where TEntity : Entity<Guid>
{
}
