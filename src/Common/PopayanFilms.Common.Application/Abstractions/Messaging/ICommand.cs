using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Application.Abstractions.Messaging;

// Marker interfaces para comandos
public interface ICommandBase<TResult>;

public interface ICommand : ICommandBase<Result>;

public interface ICommand<TResponse> : ICommandBase<Result<TResponse>>;
