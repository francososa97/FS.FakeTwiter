using FS.FakeTwiter.Application.Features.Users.Commands.CreateUserCommand;
using FS.FakeTwiter.Application.Features.Users.Commands.DeleteUserCommand;
using FS.FakeTwiter.Application.Features.Users.Commands.UpdateUserCommand;
using FS.FakeTwiter.Application.Features.Users.Queries.GetAllUsersQuery;
using FS.FakeTwiter.Application.Features.Users.Queries.GetUserByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FS.FakeTwiter.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _mediator.Send(new GetAllUsersQuery()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));
        return user is null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var response = await _mediator.Send(command);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id) return BadRequest();
        await _mediator.Send(command);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteUserCommand(id));
        return NoContent();
    }
}
