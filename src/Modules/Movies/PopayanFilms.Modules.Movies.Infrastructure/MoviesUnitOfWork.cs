using PopayanFilms.Common.Infrastructure.Persistence;
using PopayanFilms.Modules.Movies.Infrastructure.Database;

namespace PopayanFilms.Modules.Movies.Infrastructure;

internal sealed class MoviesUnitOfWork : UnitOfWork<MoviesDbContext>
{
    public MoviesUnitOfWork(MoviesDbContext context) : base(context)
    {
    }
}
