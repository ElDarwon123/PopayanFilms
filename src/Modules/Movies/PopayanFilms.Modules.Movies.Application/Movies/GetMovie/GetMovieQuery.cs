using PopayanFilms.Common.Application.Abstractions.Messaging;

namespace PopayanFilms.Modules.Movies.Application.Movies.GetMovie;

public sealed record GetMovieQuery(Guid MovieId) : IQuery<GetMovieResponse>;
