using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    public (string Token, int ExpiresIn) GenerateToken(ApplicationUser user)
    {
        Claim[] claims = [
            new (JwtRegisteredClaimNames.Sub,user.Id ),
            new (JwtRegisteredClaimNames.Email,user.Email! ),
            new (JwtRegisteredClaimNames.GivenName,user.FirstName),
            new (JwtRegisteredClaimNames.FamilyName,user.LastName),
            new (JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            ];
        var symmSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Value.Key));
        var signingCredentials = new SigningCredentials(symmSecurityKey, SecurityAlgorithms.HmacSha256);
       
        var token= new JwtSecurityToken(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(options.Value.ExpiryMinutes),
            signingCredentials: signingCredentials
            );

        return (token:new JwtSecurityTokenHandler().WriteToken(token),expiresIn: options.Value.ExpiryMinutes);
    }
}
