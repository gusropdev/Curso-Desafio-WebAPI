using Microsoft.AspNetCore.Mvc;

namespace desafio_teste.Controllers;
[ApiController]
[Route("")]
public class HomeController : ControllerBase
{
    [HttpGet("")]
    public IActionResult Get()
    {
        return Ok();
    }
}
