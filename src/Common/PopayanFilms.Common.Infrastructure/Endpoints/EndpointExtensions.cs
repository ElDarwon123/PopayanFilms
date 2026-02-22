using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace PopayanFilms.Common.Infrastructure.Endpoints;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpointMappers(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var mapperTypes = assembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && typeof(IEndpointMapper).IsAssignableFrom(t));

            foreach (var mapperType in mapperTypes)
            {
                services.AddSingleton(typeof(IEndpointMapper), mapperType);
            }
        }

        return services;
    }

    public static IEndpointRouteBuilder MapEndpointsFromMappers(this IEndpointRouteBuilder app, IServiceProvider serviceProvider)
    {
        var mappers = serviceProvider.GetServices<IEndpointMapper>();
        
        foreach (var mapper in mappers)
        {
            mapper.MapEndpoints(app);
        }

        return app;
    }
}
