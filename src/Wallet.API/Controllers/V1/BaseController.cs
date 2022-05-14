using Microsoft.AspNetCore.Mvc;
using Wallet.Infra.Others;
using Wallet.Shared.Results;
using ISession = Wallet.Domain.Interfaces.ISession;

namespace Wallet.API.Controllers.V1;

public class BaseController : Controller
{
    public BaseController(IHttpContextAccessor context)
    {
        var session = (Session)context.HttpContext!.RequestServices.GetService<ISession>()!;
        var userId = context.HttpContext.User.Identities
            .FirstOrDefault()?.Claims
            .FirstOrDefault(c => c.Type == "Id")?.Value;
        
        if(userId != null)
            session.Load(new Guid(userId));
    }

    protected IActionResult OkOrNotFoundOrBadRequest<T>(ResultData<T> result)
    {
        if (!result.IsValid)
            return GetErrors(result);

        if (result.Data == null)
            return NotFound();

        return Ok(result.Data);
    }
    
    protected IActionResult OkOrBadRequest(Result result)
    {
        if (!result.IsValid)
            return GetErrors(result);

        return Ok();
    }
    
    protected IActionResult OkOrBadRequest<T>(ResultData<T> result)
    {
        if (!result.IsValid)
            return GetErrors(result);

        if (result.Data == null)
            return Ok();

        return Ok(result.Data);
    }

    private IActionResult GetErrors(Result result)
    {
        return BadRequest(new
        {
            FieldErros = result.FieldErrors,
            Error = result.FirstError,
        });
    }
}
