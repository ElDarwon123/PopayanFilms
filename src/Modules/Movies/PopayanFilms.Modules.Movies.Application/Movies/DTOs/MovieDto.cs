namespace PopayanFilms.Modules.Movies.Application.Movies.DTOs;

public sealed record MovieDto(
    Guid Id,
    string Title,
    string Description,
    int ReleaseYear,
    int DurationMinutes,
    string Director,
    string Genre,
    decimal Rating,
    DateTime CreatedAt,
    DateTime? UpdatedAt);
