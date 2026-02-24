
namespace SurveyBasket.Api.Services;

public interface IAuthService
{
        Task<AuthResponse?> GetTokenAsync(string Email, string Password,CancellationToken cancellationToken = default);
        Task<AuthResponse?> GetRefreshTokenAsync(string token, string refreshToken,CancellationToken cancellationToken = default);
        Task<bool> RevokeRefreshTokenAsync(string token, string refreshToken,CancellationToken cancellationToken = default);
}
