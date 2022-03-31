using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SpotOps.Api.Constants;
using SpotOps.Api.Models;
using SpotOps.Api.Services.Interfaces;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SpotOps.Api.Services;

public class UserService : IUserService
{
    private readonly IJwtService _jwtService;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="roleManager"></param>
    /// <param name="jwtService"></param>
    public UserService(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtService jwtService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtService = jwtService;
    }

    /// <summary>
    ///     Registers user as ApplicationUser.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<RegisterResponseModel> RegisterAsync(RegisterModel model)
    {
        var user = new ApplicationUser
        {
            UserName = model.Username,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName
        };

        var response = new RegisterResponseModel();

        var userWithSameEmail = await _userManager.FindByEmailAsync(model.Email);

        if (userWithSameEmail != null)
        {
            response.Message = $"Email {user.Email} is already registered.";
            response.IsRegistered = false;
            return response;
        }

        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, Authorization.default_role.ToString());
        }
        else if (result.Errors.Any())
        {
            response.Message = $"{JsonSerializer.Serialize(result.Errors.First().Description)}";
            response.IsRegistered = false;
            return response;
        }

        response.Message = $"User Registered with username {user.UserName}";
        response.IsRegistered = true;

        return response;
    }

    /// <summary>
    ///     Adds role for specified user.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<string> AddRoleAsync(AddRoleModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return $"No Accounts Registered with {model.Email}.";

        if (await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roleExists = Enum.GetNames(typeof(Authorization.Roles)).Any(x => x.ToLower() == model.Role.ToLower());
            if (roleExists)
            {
                var validRole = Enum.GetValues(typeof(Authorization.Roles))
                    .Cast<Authorization.Roles>().FirstOrDefault(x => x.ToString().ToLower() == model.Role.ToLower());
                await _userManager.AddToRoleAsync(user, validRole.ToString());
                return $"Added {model.Role} to user {model.Email}.";
            }

            return $"Role {model.Role} not found.";
        }

        return $"Incorrect Credentials for user {user.Email}.";
    }

    /// <summary>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<AuthenticationModel> LoginAsync(LoginModel model)
    {
        var authenticationModel = new AuthenticationModel();
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
            return authenticationModel;
        }

        if (await _userManager.CheckPasswordAsync(user, model.Password))
        {
            authenticationModel.Message = $"Welcome to SpotOps.Api, {user.FirstName}!";
            authenticationModel.IsAuthenticated = true;
            var jwt = await CreateJwtToken(user);
            authenticationModel.Token = _jwtService.WriteToken(jwt);
            authenticationModel.Name = user.FirstName;
            authenticationModel.Email = user.Email;
            authenticationModel.UserName = user.UserName;
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            authenticationModel.Roles = rolesList.ToList();
            return authenticationModel;
        }

        authenticationModel.IsAuthenticated = false;
        authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
        return authenticationModel;
    }

    /// <summary>
    ///     Gets user using JWT token.
    /// </summary>
    /// <param name="jwt"></param>
    /// <returns></returns>
    public async Task<ApplicationUser?> GetUserFromJwtAsync(string jwt)
    {
        var jwtSecurityToken = _jwtService.Verify(jwt);
        ApplicationUser? user = null;

        if (int.TryParse(jwtSecurityToken.Issuer, out var userId))
            user = await _userManager.FindByIdAsync(userId.ToString());

        return user ?? null;
    }

    //public async Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model)
    //{
    //    var authenticationModel = new AuthenticationModel();
    //    var user = await _userManager.FindByEmailAsync(model.Email);
    //    if (user == null)
    //    {
    //        authenticationModel.IsAuthenticated = false;
    //        authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
    //        return authenticationModel;
    //    }

    //    if (await _userManager.CheckPasswordAsync(user, model.Password))
    //    {
    //        authenticationModel.IsAuthenticated = true;
    //        //JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
    //        //authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
    //        authenticationModel.Token = _jwtService.Generate(user.Id);
    //        authenticationModel.Email = user.Email;
    //        authenticationModel.UserName = user.UserName;
    //        var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
    //        authenticationModel.Roles = rolesList.ToList();
    //        return authenticationModel;
    //    }

    //    authenticationModel.IsAuthenticated = false;
    //    authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
    //    return authenticationModel;
    //}

    /// <summary>
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        return _jwtService.Generate(user, userClaims, roles);
    }

    /// <summary>
    /// TODO, handle logging out.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    // public Task<string> LogoutAsync(LogoutModel model)
    // {
    //     throw new NotImplementedException();
    // }
}