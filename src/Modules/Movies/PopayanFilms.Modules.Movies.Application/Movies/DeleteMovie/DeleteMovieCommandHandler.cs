using PopayanFilms.Common.Application.Abstractions.Data;
using PopayanFilms.Common.Application.Abstractions.Messaging;
using PopayanFilms.Common.Domain.Abstractions;
using PopayanFilms.Modules.Movies.Domain.Movies;

namespace PopayanFilms.Modules.Movies.Application.Movies.DeleteMovie;

internal sealed class DeleteMovieCommandHandler : ICommandHandler<DeleteMovieCommand, Result>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> HandleAsync(
        DeleteMovieCommand command,
        CancellationToken cancellationToken = default)
    {
        var movie = await _movieRepository.GetByIdAsync(command.MovieId, cancellationToken);

        if (movie is null)
        {
            return Result.Failure(MovieErrors.NotFound(command.MovieId));
        }

        _movieRepository.Remove(movie);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
