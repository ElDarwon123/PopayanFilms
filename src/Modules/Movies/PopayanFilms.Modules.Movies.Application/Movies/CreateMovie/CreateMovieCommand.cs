using PopayanFilms.Common.Application.Abstractions.Messaging;

namespace PopayanFilms.Modules.Movies.Application.Movies.CreateMovie;

public sealed record CreateMovieCommand(
    string Title,
    string Description,
    int ReleaseYear,
    int DurationMinutes,
    string Director,
    string Genre) : ICommand<Guid>;
