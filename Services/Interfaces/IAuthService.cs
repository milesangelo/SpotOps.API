using System.Threading.Tasks;
using SpotOps.Api.Models.Auth;

namespace SpotOps.Api.Services.Interfaces;

public interface IAuthService
{
    Task<LoginResponse?> Login(LoginRequest loginRequest);
}