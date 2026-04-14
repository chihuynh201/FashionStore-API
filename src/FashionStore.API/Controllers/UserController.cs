using FashionStore.API.Extensions;
using FashionStore.Application.Common.DTOs.Requests;
using FashionStore.Application.Features.Users.Commands.CreateUser;
using FashionStore.Application.Features.Users.Commands.DeleteUser;
using FashionStore.Application.Features.Users.Commands.Login;
using FashionStore.Application.Features.Users.Commands.UpdateUser;
using FashionStore.Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FashionStore.API.Controllers;
[ApiController]

[Authorize]
public class UserController : StoreBaseController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet()]
    public async Task<IActionResult> GetListUsers([FromQuery] QueryModel queryModel)
    {
        var result = await _mediator.Send(new GetUserListQuery(queryModel));
        return result.AsObjectResult();
    }

    [HttpGet("{userId:int:min(1)}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var result = await _mediator.Send(new GetUserByIdQuery(userId));
        return result.AsObjectResult();
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginCommand loginCommand)
    {
        var result = await _mediator.Send(loginCommand);
        return result.AsObjectResult();
    }

    [AllowAnonymous]
    [HttpPost("initial")]
    public async Task<IActionResult> CreateInitialUser(CreateInitialUserCommand userCommand)
    {
        var result = await _mediator.Send(userCommand);
        return result.AsObjectResult();
    }

    [HttpPost()]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand userCommand)
    {
        var result = await _mediator.Send(userCommand);
        return result.AsObjectResult();
    }

    [HttpPut("{userId:int:min(1)}")]
    public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserCommand userCommand)
    {
        userCommand.Id = userId;
        var result = await _mediator.Send(userCommand);
        return result.AsObjectResult();
    }


    [HttpDelete("{userId:int:min(1)}")]
    public async Task<IActionResult> DeleteUser(int userId)
    {
        var result = await _mediator.Send(new DeleteUserCommand(userId));
        return result.AsObjectResult();
    }
}
