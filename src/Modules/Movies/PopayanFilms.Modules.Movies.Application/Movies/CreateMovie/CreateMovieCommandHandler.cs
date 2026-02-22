using PopayanFilms.Common.Application.Abstractions.Data;
using PopayanFilms.Common.Application.Abstractions.Messaging;
using PopayanFilms.Common.Domain.Abstractions;
using PopayanFilms.Modules.Movies.Domain.Movies;

namespace PopayanFilms.Modules.Movies.Application.Movies.CreateMovie;

internal sealed class CreateMovieCommandHandler : ICommandHandler<CreateMovieCommand, Result<Guid>>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateMovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> HandleAsync(
        CreateMovieCommand command,
        CancellationToken cancellationToken = default)
    {
        var existingMovie = await _movieRepository.GetByTitleAsync(command.Title, cancellationToken);
        if (existingMovie is not null)
        {
            return Result.Failure<Guid>(MovieErrors.TitleAlreadyExists);
        }

        if (command.ReleaseYear < 1888 || command.ReleaseYear > DateTime.UtcNow.Year + 5)
        {
            return Result.Failure<Guid>(MovieErrors.InvalidReleaseYear);
        }

        if (command.DurationMinutes <= 0)
        {
            return Result.Failure<Guid>(MovieErrors.InvalidDuration);
        }

        var movie = Movie.Create(
            command.Title,
            command.Description,
            command.ReleaseYear,
            command.DurationMinutes,
            command.Director,
            command.Genre);

        _movieRepository.Add(movie);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return movie.Id;
    }
}
