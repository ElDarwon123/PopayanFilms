namespace PopayanFilms.Modules.Movies.Domain.Movies;

public interface IMovieRepository
{
    Task<Movie?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Movie>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Movie?> GetByTitleAsync(string title, CancellationToken cancellationToken = default);
    void Add(Movie movie);
    void Update(Movie movie);
    void Remove(Movie movie);
}
