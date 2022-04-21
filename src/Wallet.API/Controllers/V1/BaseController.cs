using Microsoft.AspNetCore.Mvc;
using Wallet.Domain.UseCases.Common.Responses;
using Wallet.Infra;
using ISession = Wallet.Domain.Interfaces.ISession;

namespace Wallet.API.Controllers.V1;

public class BaseController : Controller
{
    public BaseController(IHttpContextAccessor context)
    {
        var session = (Session)context.HttpContext!.RequestServices.GetService<ISession>()!;
        var userId = context
            .HttpContext
            .User
            .Identities
            .FirstOrDefault()?
            .Claims
            .FirstOrDefault(c => c.Type == "Id")?
            .Value;
        
        if(userId != null)
            session.Load(new Guid(userId));
    }
    
    protected IActionResult OkOrNoContent<T>(ResponseData<T> response)
    {
        if (!response.IsValid)
            return GetErrors(response);

        if (response.Data == null)
            return NoContent();

        return Ok(response.Data);
    }
    
    protected IActionResult OkOrNotFound<T>(ResponseData<T> response)
    {
        if (!response.IsValid)
            return GetErrors(response);

        if (response.Data == null)
            return NotFound();

        return Ok(response.Data);
    }
    
    protected IActionResult CreatedOrNoContent<T>(ResponseData<T> response, string uri = "")
    {
        if (!response.IsValid)
            return GetErrors(response);

        if (response.Data == null)
            return NoContent();

        return Created(uri, response.Data);
    }

    protected IActionResult NoContent(Response response)
    {
        if (!response.IsValid)
            return GetErrors(response);
        
        return NoContent();
    }

    private IActionResult GetErrors(Response response)
    {
        return BadRequest(new
        {
            FieldErros = response.FieldErrors,
            Error = response.FirstFlowError,
        });
    }
}