using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using PopayanFilms.Common.Application.Abstractions.Messaging;

namespace PopayanFilms.Common.Infrastructure.Messaging;

public static class MessagingExtensions
{
    public static IServiceCollection AddMessaging(this IServiceCollection services, params Assembly[] handlerAssemblies)
    {
        services.AddScoped<ICommandMediator, SimpleCommandMediator>();
        services.AddScoped<IQueryMediator, SimpleQueryMediator>();
        services.AddScoped<IMediator, SimpleMediator>();

        foreach (var assembly in handlerAssemblies)
        {
            services.AddCommandHandlers(assembly);
            services.AddQueryHandlers(assembly);
        }

        return services;
    }

    public static IServiceCollection AddCommandHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<,>))
                .Select(i => new { Implementation = t, Interface = i }));

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.Interface, handler.Implementation);
        }

        return services;
    }

    public static IServiceCollection AddQueryHandlers(this IServiceCollection services, Assembly assembly)
    {
        var handlerTypes = assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))
                .Select(i => new { Implementation = t, Interface = i }));

        foreach (var handler in handlerTypes)
        {
            services.AddScoped(handler.Interface, handler.Implementation);
        }

        return services;
    }
}
