using Microsoft.AspNetCore.Mvc;

namespace SpotOps.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PingController: ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "pong";
    }
}