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

   
}



