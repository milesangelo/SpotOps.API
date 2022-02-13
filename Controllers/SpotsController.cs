using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpotOps.Api.Models.Rest;
using SpotOps.Api.Services.Interfaces;

namespace SpotOps.Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class SpotsController : ControllerBase
{
    private readonly ISpotRequestService _service;

    /// <summary>
    ///     Constructs Spots Controller with Spot Response Service injection.
    /// </summary>
    /// <param name="service"></param>
    public SpotsController(ISpotRequestService service)
    {
        _service = service;
    }

    /// <summary>
    /// </summary>
    /// <param name="spot"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult> Post([FromForm] SpotRequest spot)
    {
        try
        {
            return Ok(await _service.Add(spot));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}