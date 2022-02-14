using System.Threading.Tasks;
using SpotOps.Api.Models;

namespace SpotOps.Api.Services.Interfaces;

public interface IUserService
{
    /// <summary>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<RegisterResponseModel> RegisterAsync(RegisterModel model);

    /// <summary>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<string> AddRoleAsync(AddRoleModel model);

    /// <summary>
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<AuthenticationModel> LoginAsync(LoginModel model);

    /// <summary>
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<ApplicationUser?> GetUserFromJwtAsync(string jwt);

    //Task<string> LogoutAsync(LogoutModel model);
}