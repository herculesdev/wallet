using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Domain.UseCases.Commands.Requests;

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
        return OkOrBadRequest(await _mediator.Send(command));
    }
    
    [Authorize(Roles = "admin")]
    [HttpPost("deposit")]
    public async Task<IActionResult> Send(CreateDepositCommand command)
    {
        return OkOrBadRequest(await _mediator.Send(command));
    }
}
