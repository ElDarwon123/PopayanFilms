using PopayanFilms.Modules.Movies.Application.Movies.DTOs;

namespace PopayanFilms.Modules.Movies.Application.Movies.GetMovies;

public sealed record GetMoviesResponse(IReadOnlyList<MovieDto> Movies);
