using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Authentication;

namespace SurveyBasket.Api.Services;

public class AuthService(UserManager<ApplicationUser> userManager,IJwtProvider jwtProvider) : IAuthService
{
    public async Task<AuthResponse?> GetTokenAsync(string Email, string Password, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(Email);
        if(user == null)
        {
            return null;
        }
        var isPasswordValid = await userManager.CheckPasswordAsync(user, Password);
        if (!isPasswordValid)
        {
            return null;
        }
        var (token, expiresIn) = jwtProvider.GenerateToken(user);
        return new AuthResponse(user.Id,user.Email,user.FirstName,user.LastName, token,expiresIn*60);
        
        
    }
}
