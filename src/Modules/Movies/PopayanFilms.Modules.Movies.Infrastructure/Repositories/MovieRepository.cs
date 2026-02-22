using Microsoft.EntityFrameworkCore;
using PopayanFilms.Modules.Movies.Domain.Movies;
using PopayanFilms.Modules.Movies.Infrastructure.Database;

namespace PopayanFilms.Modules.Movies.Infrastructure.Repositories;

internal sealed class MovieRepository : IMovieRepository
{
    private readonly MoviesDbContext _context;

    public MovieRepository(MoviesDbContext context)
    {
        _context = context;
    }

    public async Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Movies.FindAsync([id], cancellationToken);
    }

    public async Task<IReadOnlyList<Movie>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Movies.ToListAsync(cancellationToken);
    }

    public async Task<Movie?> GetByTitleAsync(string title, CancellationToken cancellationToken = default)
    {
        return await _context.Movies.FirstOrDefaultAsync(
            m => m.Title.ToLower() == title.ToLower(),
            cancellationToken);
    }

    public void Add(Movie movie)
    {
        _context.Movies.Add(movie);
    }

    public void Update(Movie movie)
    {
        _context.Movies.Update(movie);
    }

    public void Remove(Movie movie)
    {
        _context.Movies.Remove(movie);
    }
}
