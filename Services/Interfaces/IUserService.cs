using System.Threading.Tasks;
using SpotOps.Api.Models;

namespace SpotOps.Api.Services.Interfaces;

public interface IUserService
{
    Task<string> RegisterAsync(RegisterModel model);
    Task<AuthenticationModel> GetTokenAsync(TokenRequestModel model);
    
    Task<string> AddRoleAsync(AddRoleModel model);
}