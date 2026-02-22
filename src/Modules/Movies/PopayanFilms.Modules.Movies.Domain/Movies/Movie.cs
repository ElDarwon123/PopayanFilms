using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Modules.Movies.Domain.Movies;

public sealed class Movie : AggregateRoot<Guid>
{
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public int ReleaseYear { get; private set; }
    public int DurationMinutes { get; private set; }
    public string Director { get; private set; } = string.Empty;
    public string Genre { get; private set; } = string.Empty;
    public decimal Rating { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private Movie() { }

    private Movie(
        Guid id,
        string title,
        string description,
        int releaseYear,
        int durationMinutes,
        string director,
        string genre) : base(id)
    {
        Title = title;
        Description = description;
        ReleaseYear = releaseYear;
        DurationMinutes = durationMinutes;
        Director = director;
        Genre = genre;
        Rating = 0;
        CreatedAt = DateTime.UtcNow;
    }

    public static Movie Create(
        string title,
        string description,
        int releaseYear,
        int durationMinutes,
        string director,
        string genre)
    {
        var movie = new Movie(
            Guid.NewGuid(),
            title,
            description,
            releaseYear,
            durationMinutes,
            director,
            genre);

        movie.RaiseDomainEvent(new MovieCreatedDomainEvent(movie.Id, movie.Title));

        return movie;
    }

    public void Update(
        string title,
        string description,
        int releaseYear,
        int durationMinutes,
        string director,
        string genre)
    {
        Title = title;
        Description = description;
        ReleaseYear = releaseYear;
        DurationMinutes = durationMinutes;
        Director = director;
        Genre = genre;
        UpdatedAt = DateTime.UtcNow;

        RaiseDomainEvent(new MovieUpdatedDomainEvent(Id, Title));
    }

    public void UpdateRating(decimal rating)
    {
        if (rating < 0 || rating > 10)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 0 and 10.");

        Rating = rating;
        UpdatedAt = DateTime.UtcNow;
    }
}
