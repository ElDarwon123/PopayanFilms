using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Modules.Movies.Domain.Movies;

public sealed record MovieCreatedDomainEvent(Guid MovieId, string Title) : DomainEvent;

public sealed record MovieUpdatedDomainEvent(Guid MovieId, string Title) : DomainEvent;

public sealed record MovieDeletedDomainEvent(Guid MovieId) : DomainEvent;
