using PopayanFilms.Common.Application.Abstractions.Messaging;
using PopayanFilms.Common.Domain.Abstractions;
using PopayanFilms.Modules.Movies.Application.Movies.DTOs;
using PopayanFilms.Modules.Movies.Domain.Movies;

namespace PopayanFilms.Modules.Movies.Application.Movies.GetMovie;

internal sealed class GetMovieQueryHandler : IQueryHandler<GetMovieQuery, Result<GetMovieResponse>>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<Result<GetMovieResponse>> HandleAsync(
        GetMovieQuery query,
        CancellationToken cancellationToken = default)
    {
        var movie = await _movieRepository.GetByIdAsync(query.MovieId, cancellationToken);

        if (movie is null)
        {
            return Result.Failure<GetMovieResponse>(MovieErrors.NotFound(query.MovieId));
        }

        var movieDto = new MovieDto(
            movie.Id,
            movie.Title,
            movie.Description,
            movie.ReleaseYear,
            movie.DurationMinutes,
            movie.Director,
            movie.Genre,
            movie.Rating,
            movie.CreatedAt,
            movie.UpdatedAt);

        return new GetMovieResponse(movieDto);
    }
}
