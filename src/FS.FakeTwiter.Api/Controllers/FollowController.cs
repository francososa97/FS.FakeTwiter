using FS.FakeTwitter.Application.Features.Follows.Commands;
using FS.FakeTwitter.Application.Features.Follows.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FS.FakeTwitter.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FollowController : ControllerBase
{
    private readonly IMediator _mediator;

    public FollowController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Permite a un usuario seguir a otro usuario.
    /// </summary>
    /// <param name="command">Contiene los IDs del seguidor y del seguido.</param>
    /// <response code="200">Follow realizado correctamente</response>
    /// <response code="400">Datos inválidos</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> FollowUser([FromBody] FollowUserCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    /// <summary>
    /// Obtiene la lista de usuarios que siguen a un usuario determinado.
    /// </summary>
    /// <param name="userId">ID del usuario del que se desea obtener los seguidores.</param>
    /// <returns>Lista de IDs de usuarios que siguen al usuario especificado.</returns>
    /// <response code="200">Lista de seguidores obtenida correctamente</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpGet("followers/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFollowers(string userId)
    {
        var result = await _mediator.Send(new GetFollowersQuery { UserId = userId });
        return Ok(result);
    }

    /// <summary>
    /// Obtiene la lista de usuarios a los que sigue un usuario determinado.
    /// </summary>
    /// <param name="userId">ID del usuario del que se desea obtener los seguidos.</param>
    /// <returns>Lista de IDs de usuarios a los que sigue el usuario especificado.</returns>
    /// <response code="200">Lista de seguidos obtenida correctamente</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpGet("following/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetFollowing(string userId)
    {
        var result = await _mediator.Send(new GetFollowingQuery { UserId = userId });
        return Ok(result);
    }
}
