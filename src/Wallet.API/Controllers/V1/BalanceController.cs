using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Domain.Queries.Requests;

namespace Wallet.API.Controllers.V1;

[ApiController]
[Route("v1/[controller]")]
public class BalanceController : BaseController
{
    private readonly IMediator _mediator;
    public BalanceController(IHttpContextAccessor contextAccessor, IMediator mediator) : base(contextAccessor)
    {
        _mediator = mediator;
    }
    
    [Authorize]
    [HttpPost("history")]
    public async Task<IActionResult> Send(GetBalanceHistoryQuery query)
    {
        return OkOrBadRequest(await _mediator.Send(query));
    }
}
