﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Domain.Commands.Requests;
using Wallet.Domain.Queries.Requests;
using Wallet.Domain.Responses;
using Wallet.Shared.Others;

namespace Wallet.API.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
public class UsersController : BaseController
{
    private readonly IMediator _mediator;
    public UsersController(IHttpContextAccessor contextAccessor, IMediator mediator) : base(contextAccessor)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [ProducesResponseType(typeof(UserResponse), 201)]
    [HttpPost]
    public async Task<IActionResult> AddAsync(CreateUserCommand command)
    {
        return OkOrBadRequest(await _mediator.Send(command));
    }

    #region Auth

    [HttpPost("auth")]
    public async Task<IActionResult> Auth(AuthCommand command)
    {
        var response = await _mediator.Send(command);
        return OkOrNotFoundOrBadRequest(response);
    }

    #endregion
    
    #region Admin
    [Authorize(Roles = "admin")]
    [HttpGet("/v1/admin/[controller]/{id}")]
    [ProducesResponseType(typeof(UserResponse), 200)]
    public async Task<IActionResult> GetById(Guid id)
    {
        return OkOrNotFoundOrBadRequest(await _mediator.Send(new GetUserByIdQuery(id)));
    }
    
    [Authorize(Roles = "admin")]
    [ProducesResponseType(typeof(PagedResult<UserResponse>), 200)]
    [HttpGet("/v1/admin/[controller]/")]
    public async Task<IActionResult> GetAll([FromQuery]GetAllUserQuery query)
    {
        return OkOrBadRequest(await _mediator.Send(query));
    }
    
    [Authorize(Roles = "admin")]
    [HttpPatch("/v1/admin/[controller]/{userId}/approve")]
    public async Task<IActionResult> Approve(Guid userId)
    {
        return OkOrBadRequest(await _mediator.Send(new ApproveUserCommand(userId)));
    }
    
    [Authorize(Roles = "admin")]
    [HttpPatch("/v1/admin/[controller]/{userId}/disapprove")]
    public async Task<IActionResult> Disapprove(Guid userId)
    {
        return OkOrBadRequest(await _mediator.Send(new DisapproveUserCommand(userId)));
    }
    
    #endregion
}
