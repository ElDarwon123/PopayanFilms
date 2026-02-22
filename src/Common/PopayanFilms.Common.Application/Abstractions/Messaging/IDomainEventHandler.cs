using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Application.Abstractions.Messaging;

public interface IDomainEventHandler<in TDomainEvent>
    where TDomainEvent : IDomainEvent
{
    Task HandleAsync(TDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
