using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PopayanFilms.Common.Application.Abstractions.Messaging;
using PopayanFilms.Common.Domain.Abstractions;

namespace PopayanFilms.Common.Infrastructure.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class ApiControllerBase : ControllerBase
{
    protected readonly IMediator Mediator;

    protected ApiControllerBase(IMediator mediator)
    {
        Mediator = mediator;
    }

    protected IActionResult HandleResult<T>(Result<T> result)
    {
        if (result.IsSuccess)
            return Ok(result.Value);

        return HandleError(result.Error);
    }

    protected IActionResult HandleResult(Result result)
    {
        if (result.IsSuccess)
            return NoContent();

        return HandleError(result.Error);
    }

    protected IActionResult HandleCreatedResult<T>(Result<T> result, string actionName, object? routeValues = null)
    {
        if (result.IsSuccess)
            return CreatedAtAction(actionName, routeValues, result.Value);

        return HandleError(result.Error);
    }

    protected IActionResult HandleError(Error error)
    {
        if (error.Code.Contains("NotFound", StringComparison.OrdinalIgnoreCase))
        {
            return NotFound(new ProblemDetails
            {
                Title = "Not Found",
                Detail = error.Description,
                Status = StatusCodes.Status404NotFound
            });
        }

        if (error.Code.Contains("Forbidden", StringComparison.OrdinalIgnoreCase))
        {
            return Forbid();
        }

        if (error.Code.Contains("Unauthorized", StringComparison.OrdinalIgnoreCase))
        {
            return Unauthorized(new ProblemDetails
            {
                Title = "Unauthorized",
                Detail = error.Description,
                Status = StatusCodes.Status401Unauthorized
            });
        }

        if (error.Code.Contains("Conflict", StringComparison.OrdinalIgnoreCase))
        {
            return Conflict(new ProblemDetails
            {
                Title = "Conflict",
                Detail = error.Description,
                Status = StatusCodes.Status409Conflict
            });
        }

        return BadRequest(new ProblemDetails
        {
            Title = "Bad Request",
            Detail = error.Description,
            Status = StatusCodes.Status400BadRequest
        });
    }
}
