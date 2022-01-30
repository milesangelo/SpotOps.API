using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SpotOps.Api.Models;

namespace SpotOps.Api.Services;

public class LoginService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ILogger<LoginService> _logger;

    public LoginService(SignInManager<ApplicationUser> signInManager, ILogger<LoginService> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task OnGetAsync(string returnUrl = null)
    {
        // Clear the existing external cookie to ensure a clean login process
        // await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
        //
        // ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        //
        // ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string email, string password, bool rememberMe)
    {
//        returnUrl ??= Url.Content("~/");

        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            _logger.LogInformation(LoggerEventIds.UserLogin, "User logged in.");
            //return LocalRedirect(returnUrl);
        }

        if (result.RequiresTwoFactor)
        {
            //return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
        }

        if (result.IsLockedOut)
        {
            _logger.LogWarning(LoggerEventIds.UserLockout, "User account locked out.");
            //return ""RedirectToPage("./Lockout");
        }
        else
        {
            //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            //return Page();
        }

        return new OkResult();
    }
}