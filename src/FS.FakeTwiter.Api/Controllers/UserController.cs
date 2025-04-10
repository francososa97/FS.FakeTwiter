using FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;
using FS.FakeTwiter.Application.Features.Users.Commands.DeleteUserCommand;
using FS.FakeTwiter.Application.Features.Users.Commands.UpdateUserCommand;
using FS.FakeTwiter.Application.Features.Users.Queries.GetAllUsersQuery;
using FS.FakeTwiter.Application.Features.Users.Queries.GetUserByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FS.FakeTwiter.Api.Controllers;

/// <summary>
/// Controlador para operaciones sobre usuarios (CRUD).
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los usuarios registrados
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllUsersQuery()));


    /// <summary>
    /// Obtiene un usuario por su ID.
    /// </summary>
    /// <param name="id">Identificador del usuario.</param>
    /// <returns>Usuario encontrado o 404 si no existe.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return user is null ? NotFound() : Ok(user);
    }

    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="command">Datos del usuario a crear.</param>
    /// <returns>Usuario creado</returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Actualiza un usuario existente.
    /// </summary>
    /// <param name="id">ID del usuario a actualizar.</param>
    /// <param name="command">Datos actualizados del usuario.</param>
    /// <returns>204 si fue exitoso o 400 si hay inconsistencia en el ID.</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id) return BadRequest();
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    /// <summary>
    /// Elimina un usuario por ID (soft delete).
    /// </summary>
    /// <param name="id">ID del usuario a eliminar.</param>
    /// <returns>204 si fue eliminado.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }
}
