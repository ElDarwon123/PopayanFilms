namespace PopayanFilms.Modules.Movies.Api.Contracts;

public sealed record CreateMovieRequest(
    string Title,
    string Description,
    int ReleaseYear,
    int DurationMinutes,
    string Director,
    string Genre);
