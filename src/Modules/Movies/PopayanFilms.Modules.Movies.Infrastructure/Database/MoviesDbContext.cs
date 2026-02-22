using Microsoft.EntityFrameworkCore;
using PopayanFilms.Modules.Movies.Domain.Movies;

namespace PopayanFilms.Modules.Movies.Infrastructure.Database;

public sealed class MoviesDbContext : DbContext
{
    public DbSet<Movie> Movies => Set<Movie>();

    public MoviesDbContext(DbContextOptions<MoviesDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Movies);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MoviesDbContext).Assembly);
    }
}

internal static class Schemas
{
    public const string Movies = "movies";
}
