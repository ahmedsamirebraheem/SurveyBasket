using Microsoft.AspNetCore.Identity;
using SurveyBasket.Api.Authentication;
using System.Security.Cryptography;

namespace SurveyBasket.Api.Services;

public class AuthService(UserManager<ApplicationUser> userManager,IJwtProvider jwtProvider) : IAuthService
{
    private readonly int _refreshTokenExpiryDays = 14;
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
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
        var (token, expiresIn) = jwtProvider.GenerateToken(user);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration
        });
        await userManager.UpdateAsync(user);
        return new AuthResponse(user.Id,user.Email,user.FirstName,user.LastName, token,expiresIn*60,refreshToken, refreshTokenExpiration);
        
        
    }
    public async Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = jwtProvider.ValidateToken(token);
        if (userId == null) { return null; }

        var user = await userManager.FindByIdAsync(userId);
        if (user == null) { return null; }

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive);
        if (userRefreshToken == null) { return null; }
        userRefreshToken.RevokedOn = DateTime.UtcNow;

        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);
        var (newToken, expiresIn) = jwtProvider.GenerateToken(user);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });
        await userManager.UpdateAsync(user);

        return new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn * 60, newRefreshToken, refreshTokenExpiration);

    }
    public async Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken = default)
    {
        var userId = jwtProvider.ValidateToken(token);
        if (userId == null) { return false; }

        var user = await userManager.FindByIdAsync(userId);
        if (user == null) { return false; }

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(rt => rt.Token == refreshToken && rt.IsActive);
        if (userRefreshToken == null) { return false; }
        userRefreshToken.RevokedOn = DateTime.UtcNow;

        await userManager.UpdateAsync(user);
        return true;

    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

}
