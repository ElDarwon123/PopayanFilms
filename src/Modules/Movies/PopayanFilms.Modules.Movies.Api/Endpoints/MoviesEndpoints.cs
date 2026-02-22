using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PopayanFilms.Common.Application.Abstractions.Messaging;
using PopayanFilms.Modules.Movies.Api.Contracts;
using PopayanFilms.Modules.Movies.Application.Movies.CreateMovie;
using PopayanFilms.Modules.Movies.Application.Movies.DeleteMovie;
using PopayanFilms.Modules.Movies.Application.Movies.GetMovie;
using PopayanFilms.Modules.Movies.Application.Movies.GetMovies;

namespace PopayanFilms.Modules.Movies.Api.Endpoints;

public static class MoviesEndpoints
{
    public static void MapMoviesEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/movies").WithTags("Movies");

        group.MapGet("/", GetMovies)
            .WithName("GetMovies")
            .Produces<GetMoviesResponse>();

        group.MapGet("/{id:guid}", GetMovie)
            .WithName("GetMovie")
            .Produces<GetMovieResponse>()
            .Produces(StatusCodes.Status404NotFound);

        group.MapPost("/", CreateMovie)
            .WithName("CreateMovie")
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

        group.MapDelete("/{id:guid}", DeleteMovie)
            .WithName("DeleteMovie")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
    }

    private static async Task<IResult> GetMovies(IQueryMediator queryMediator, CancellationToken cancellationToken)
    {
        var result = await queryMediator.QueryAsync(new GetMoviesQuery(), cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> GetMovie(
        Guid id,
        IQueryMediator queryMediator,
        CancellationToken cancellationToken)
    {
        var result = await queryMediator.QueryAsync(new GetMovieQuery(id), cancellationToken);

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.NotFound(result.Error);
    }

    private static async Task<IResult> CreateMovie(
        CreateMovieRequest request,
        ICommandMediator commandMediator,
        CancellationToken cancellationToken)
    {
        var command = new CreateMovieCommand(
            request.Title,
            request.Description,
            request.ReleaseYear,
            request.DurationMinutes,
            request.Director,
            request.Genre);

        var result = await commandMediator.SendAsync(command, cancellationToken);

        return result.IsSuccess
            ? Results.Created($"/api/movies/{result.Value}", result.Value)
            : Results.BadRequest(result.Error);
    }

    private static async Task<IResult> DeleteMovie(
        Guid id,
        ICommandMediator commandMediator,
        CancellationToken cancellationToken)
    {
        var result = await commandMediator.SendAsync(new DeleteMovieCommand(id), cancellationToken);

        return result.IsSuccess
            ? Results.NoContent()
            : Results.NotFound(result.Error);
    }
}
