using PopayanFilms.Common.Application.Abstractions.Messaging;

namespace PopayanFilms.Modules.Movies.Application.Movies.GetMovies;

public sealed record GetMoviesQuery : IQuery<GetMoviesResponse>;
