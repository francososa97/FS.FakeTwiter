using FS.FakeTwiter.Application.Features.Tweet.Commands.PostTweetCommand;
using FS.FakeTwitter.Application.Features.Tweets.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FS.FakeTwitter.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TweetController : ControllerBase
{
    private readonly IMediator _mediator;

    public TweetController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Publica un nuevo tweet.
    /// </summary>
    /// <param name="command">Datos del tweet: ID del usuario y contenido del mensaje.</param>
    /// <returns>Id del tweet creado.</returns>
    /// <response code="200">Tweet creado correctamente</response>
    /// <response code="400">Contenido inválido</response>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostTweet([FromBody] PostTweetCommand command)
    {
        var tweetId = await _mediator.Send(command);
        return Ok(new { tweetId });
    }

    /// <summary>
    /// Obtiene todos los tweets publicados por un usuario.
    /// </summary>
    /// <param name="userId">ID del usuario.</param>
    /// <returns>Lista de tweets en formato texto.</returns>
    /// <response code="200">Tweets obtenidos correctamente</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserTweets(string userId)
    {
        var result = await _mediator.Send(new GetUserTweetsQuery { UserId = userId });
        return Ok(result);
    }

    /// <summary>
    /// Obtiene el timeline (tweets) de los usuarios que sigue un usuario específico.
    /// </summary>
    /// <param name="userId">ID del usuario que consulta el timeline.</param>
    /// <returns>Lista de tweets ordenados por fecha descendente.</returns>
    /// <response code="200">Timeline obtenido correctamente</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpGet("timeline/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTimeline(string userId)
    {
        var result = await _mediator.Send(new GetTimelineQuery { UserId = userId });
        return Ok(result);
    }
}
