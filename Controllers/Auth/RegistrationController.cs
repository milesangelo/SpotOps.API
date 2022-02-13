using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpotOps.Api.Models.Auth;
using SpotOps.Api.Services.Interfaces;

namespace SpotOps.Api.Controllers.Auth;

[Route("api/[controller]")]
[ApiController]
public class RegistrationController : ControllerBase
{
    /// <summary>
    ///     Authorization Service
    /// </summary>
    private readonly IAuthService _authService;

    /// <summary>
    ///     Construct Registration Controller with given AuthService
    /// </summary>
    /// <param name="authService"></param>
    public RegistrationController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    ///     Handle login using given LoginRequest.
    /// </summary>
    /// <param name="loginRequest"></param>
    /// <returns></returns>
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        if (loginRequest == null || string.IsNullOrEmpty(loginRequest.Username) ||
            string.IsNullOrEmpty(loginRequest.Password))
            return BadRequest("Missing login details");

        var loginResponse = await _authService.Login(loginRequest);

        if (loginResponse == null) return BadRequest("Invalid credentials");

        return Ok(loginResponse);
    }
}