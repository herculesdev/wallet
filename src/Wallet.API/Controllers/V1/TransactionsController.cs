using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Domain.Entities.Base;
using Wallet.Domain.UseCases.Commands.Requests;
using Wallet.Domain.UseCases.Common.Responses;
using Wallet.Domain.UseCases.Queries.Requests;

namespace Wallet.API.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
public class TransactionsController : BaseController
{
    private readonly IMediator _mediator;
    public TransactionsController(IHttpContextAccessor contextAccessor, IMediator mediator) : base(contextAccessor)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpPost("transfers")]
    public async Task<IActionResult> Send(CreateTransferCommand command)
    {
        return NoContent(await _mediator.Send(command));
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost("deposit")]
    public async Task<IActionResult> Send(CreateDepositCommand command)
    {
        return NoContent(await _mediator.Send(command));
    }
}
