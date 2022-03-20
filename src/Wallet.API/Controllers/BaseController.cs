using Microsoft.AspNetCore.Mvc;
using Wallet.Domain.UseCases.Common.Responses;

namespace Wallet.API.Controllers;

public class BaseController : Controller
{
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

    protected new IActionResult NoContent(Response response)
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
