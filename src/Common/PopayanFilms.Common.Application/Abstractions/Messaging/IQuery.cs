using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Application.Abstractions.Messaging;

// Marker interface para queries
public interface IQueryBase<TResult>;

public interface IQuery<TResponse> : IQueryBase<Result<TResponse>>;
