using Microsoft.AspNetCore.Mvc;

namespace SpotOps.Api.Controllers;

[Route("ping")]
[ApiController]
public class PingController: ControllerBase
{
    [HttpGet]
    public string Get()
    {
        return "pong";
    }
}