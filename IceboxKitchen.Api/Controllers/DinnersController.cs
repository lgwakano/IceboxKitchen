using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IceboxKitchen.Api.Controllers;

[Route("[controller]")]
[Authorize]
public class DinnersController : ApiController
{
    [HttpGet]
    public IActionResult GetDinners()
    {
        return Ok(Array.Empty<string>());
        //return Ok(new List<string> { "Dinner 1", "Dinner 2" });
    }
}