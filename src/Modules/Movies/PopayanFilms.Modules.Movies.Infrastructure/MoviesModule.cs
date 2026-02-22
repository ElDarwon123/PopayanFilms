using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PopayanFilms.Common.Application.Abstractions.Data;
using PopayanFilms.Common.Infrastructure.Messaging;
using PopayanFilms.Modules.Movies.Domain.Movies;
using PopayanFilms.Modules.Movies.Infrastructure.Database;
using PopayanFilms.Modules.Movies.Infrastructure.Repositories;

namespace PopayanFilms.Modules.Movies.Infrastructure;

public static class MoviesModule
{
    public static IServiceCollection AddMoviesModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        services.AddMoviesMessaging();

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database")
            ?? "Data Source=popayanfilms.db";

        services.AddDbContext<MoviesDbContext>(options =>
            options.UseSqlite(connectionString));

        services.AddScoped<IUnitOfWork, MoviesUnitOfWork>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMovieRepository, MovieRepository>();
    }

    private static void AddMoviesMessaging(this IServiceCollection services)
    {
        var applicationAssembly = typeof(Application.Movies.GetMovie.GetMovieQuery).Assembly;

        services.AddMessaging(applicationAssembly);
    }
}
