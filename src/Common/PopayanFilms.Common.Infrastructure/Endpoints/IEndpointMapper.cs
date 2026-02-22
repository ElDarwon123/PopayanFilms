using Microsoft.AspNetCore.Routing;

namespace PopayanFilms.Common.Infrastructure.Endpoints;

public interface IEndpointMapper
{
    void MapEndpoints(IEndpointRouteBuilder app);
}
