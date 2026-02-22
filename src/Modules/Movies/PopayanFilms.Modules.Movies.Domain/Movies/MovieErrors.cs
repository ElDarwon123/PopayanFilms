using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Modules.Movies.Domain.Movies;

public static class MovieErrors
{
    public static Error NotFound(Guid movieId) => new(
        "Movies.NotFound",
        $"The movie with ID '{movieId}' was not found.");

    public static readonly Error TitleAlreadyExists = new(
        "Movies.TitleAlreadyExists",
        "A movie with this title already exists.");

    public static readonly Error InvalidReleaseYear = new(
        "Movies.InvalidReleaseYear",
        "The release year is invalid.");

    public static readonly Error InvalidDuration = new(
        "Movies.InvalidDuration",
        "The duration must be greater than zero.");
}
