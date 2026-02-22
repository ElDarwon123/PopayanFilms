using Microsoft.EntityFrameworkCore;
using PopayanFilms.Common.Application.Abstractions.Data;

namespace PopayanFilms.Common.Infrastructure.Persistence;

public abstract class UnitOfWork<TContext> : IUnitOfWork
    where TContext : DbContext
{
    protected readonly TContext Context;

    protected UnitOfWork(TContext context)
    {
        Context = context;
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await Context.SaveChangesAsync(cancellationToken);
    }
}
