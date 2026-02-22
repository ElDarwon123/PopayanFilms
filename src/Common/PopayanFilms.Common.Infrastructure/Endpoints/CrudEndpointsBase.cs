using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PopayanFilms.Common.Application.Abstractions.Messaging;
using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Infrastructure.Endpoints;

public abstract class CrudEndpointsBase<TId>
    where TId : notnull
{
    protected abstract string RoutePrefix { get; }
    protected abstract string Tag { get; }

    protected static IResult HandleResult<T>(Result<T> result)
    {
        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error);
    }

    protected static IResult HandleResult(Result result)
    {
        return result.IsSuccess
            ? Results.NoContent()
            : Results.BadRequest(result.Error);
    }

    protected static IResult HandleCreatedResult<T>(Result<T> result, string routeName, Func<T, object> routeValues)
    {
        return result.IsSuccess
            ? Results.CreatedAtRoute(routeName, routeValues(result.Value), result.Value)
            : Results.BadRequest(result.Error);
    }

    protected static IResult HandleNotFoundResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Results.Ok(result.Value);

        return result.Error.Code.Contains("NotFound", StringComparison.OrdinalIgnoreCase)
            ? Results.NotFound(result.Error)
            : Results.BadRequest(result.Error);
    }
}

public abstract class CrudEndpointsBase : CrudEndpointsBase<Guid>
{
}
