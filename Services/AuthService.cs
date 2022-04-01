using System.Linq;
using System.Threading.Tasks;
using SpotOps.Api.Data;
using SpotOps.Api.Helpers;
using SpotOps.Api.Models.Auth;
using SpotOps.Api.Services.Interfaces;

namespace SpotOps.Api.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<LoginResponse?> Login(LoginRequest loginRequest)
    {
        var user = _context.Users.SingleOrDefault(user =>
            user.Active == 1 && user.UserName == loginRequest.Username);

        if (user == null) return null;

        var passwordHash = HashingHelper.HashUsingPbkdf2(loginRequest.Password, user.PasswordSalt);

        if (user.Password != passwordHash) return null;

        var token = await Task.Run(() => TokenHelper.GenerateToken(user));

        return new LoginResponse
        {
            Username = user.UserName, FirstName = user.FirstName, LastName = user.LastName,
            Token = token
        };
    }
}