using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotOps.Api.Models;
using SpotOps.Api.Services.Interfaces;

namespace SpotOps.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    /// <summary>
    /// The user service.
    /// </summary>
    private readonly IUserService _userService;
    
    /// <summary>
    /// Constructs a User Controller to use the given IUserService object.
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Asynchronously registers a user using RegisterModel object.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterModel model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }
    
    /// <summary>
    /// Asynchronously gets token for given TokenRequestModel object.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(TokenRequestModel model)
    {
        var result = await _userService.GetTokenAsync(model);
        return Ok(result);
    }
    
    /// <summary>
    /// Asynchronously adds role using AddRoleModel object data.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }
}