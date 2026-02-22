using PopayanFilms.Common.Application.Abstractions.Messaging;
using PopayanFilms.Common.Domain.Abstractions;
using PopayanFilms.Modules.Movies.Application.Movies.DTOs;
using PopayanFilms.Modules.Movies.Domain.Movies;

namespace PopayanFilms.Modules.Movies.Application.Movies.GetMovies;

internal sealed class GetMoviesQueryHandler : IQueryHandler<GetMoviesQuery, Result<GetMoviesResponse>>
{
    private readonly IMovieRepository _movieRepository;

    public GetMoviesQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Result<GetMoviesResponse>> HandleAsync(
        GetMoviesQuery query,
        CancellationToken cancellationToken = default)
    {
        var movies = await _movieRepository.GetAllAsync(cancellationToken);

        var movieDtos = movies.Select(movie => new MovieDto(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.ReleaseYear,
            movie.DurationMinutes,
            movie.Director,
            movie.Genre,
            movie.Rating,
            movie.CreatedAt,
            movie.UpdatedAt)).ToList();

        return new GetMoviesResponse(movieDtos);
    }
}
