using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using SpotOps.Api.Helpers;
using SpotOps.Api.Models;
using SpotOps.Api.Services.Interfaces;
using SpotOps.Api.Settings;

namespace SpotOps.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    /// <summary>
    ///     The user service.
    /// </summary>
    private readonly IUserService _userService;

    /// <summary>
    ///     Constructs a User Controller to use the given IUserService object.
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    ///     Asynchronously registers a user using RegisterModel object.
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
    ///     Asynchronously adds role using AddRoleModel object data.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost("addrole")]
    public async Task<IActionResult> AddRoleAsync(AddRoleModel model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }

    /// <summary>
    /// </summary>
    /// <param name="model">AddRoleModel containing user info.</param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync(LoginModel model)
    {
        var result = await _userService.LoginAsync(model);

        return Ok(result);
    }

    /// <summary>
    /// Get user information
    /// </summary>
    /// <returns></returns>
    [HttpGet("user")]
    public async Task<IActionResult> GetUserAsync()
    {
        var jwt = Request.Cookies["jwt"];
        var user = await _userService.GetUserFromJwtAsync(jwt);
        return Ok(user);
    }

    /// <summary>
    /// Logout endpoint
    /// </summary>
    /// <returns></returns>
    [HttpPost("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        Response.Cookies.Delete("jwt");

        return Ok(new
        {
            message = "Success"
        });
    }

}