using Microsoft.AspNetCore.Mvc;
using PopayanFilms.Common.Application.Abstractions.Messaging;
using PopayanFilms.Common.Infrastructure.Controllers;
using PopayanFilms.Modules.Movies.Api.Contracts;
using PopayanFilms.Modules.Movies.Application.Movies.CreateMovie;
using PopayanFilms.Modules.Movies.Application.Movies.DeleteMovie;
using PopayanFilms.Modules.Movies.Application.Movies.GetMovie;
using PopayanFilms.Modules.Movies.Application.Movies.GetMovies;

namespace PopayanFilms.Modules.Movies.Api.Controllers;

[Route("api/[controller]")]
public class MoviesController : ApiControllerBase
{
    public MoviesController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMovies(CancellationToken cancellationToken)
    {
        var result = await Mediator.QueryAsync(new GetMoviesQuery(), cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetMovie([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.QueryAsync(new GetMovieQuery(id), cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMovie([FromBody] CreateMovieRequest request, CancellationToken cancellationToken)
    {
        var command = new CreateMovieCommand(
            request.Title,
            request.Description,
            request.ReleaseYear,
            request.DurationMinutes,
            request.Director,
            request.Genre);

        var result = await Mediator.SendAsync(command, cancellationToken);
        
        return HandleCreatedResult(result, nameof(GetMovie), new { id = result.IsSuccess ? result.Value : Guid.Empty });
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteMovie([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.SendAsync(new DeleteMovieCommand(id), cancellationToken);
        return HandleResult(result);
    }
}
