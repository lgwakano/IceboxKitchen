using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace IceboxKitchen.Api.Controllers;

public class ErrorsController : ApiController
{
    [Route("/error")]
    public IActionResult Error()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        var exception = context?.Error; // Your exception

        // Log the exception or handle it as needed

        return Problem(
            detail: exception?.Message,
            title: "An error occurred while processing your request.");
    }
}