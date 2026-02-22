using PopayanFilms.Common.Application.Abstractions.Messaging;

namespace PopayanFilms.Modules.Movies.Application.Movies.DeleteMovie;

public sealed record DeleteMovieCommand(Guid MovieId) : ICommand;
