using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SurveyBasket.Api.Authentication;

namespace SurveyBasket.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController(IAuthService authService,IOptions<JwtOptions> jwtOptions) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var authResult = await authService.GetTokenAsync(request.Email, request.Password, cancellationToken);
        
        return authResult is null? BadRequest("Invalid Emil and Password"): Ok(authResult);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshAsync([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var authResult = await authService.GetRefreshTokenAsync(request.token, request.refreshToken, cancellationToken);

        return authResult is null ? BadRequest("Invalid Token") : Ok(authResult);
    }


    [HttpPost("revoke-refresh-token")]
    public async Task<IActionResult> RevokeRefreshTokenAsync([FromBody]RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var isRevoked = await authService.RevokeRefreshTokenAsync(request.token, request.refreshToken, cancellationToken);

        return isRevoked ? Ok():BadRequest("Operation failed");
    }

}



