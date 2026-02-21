using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SurveyBasket.Api.Authentication;

public class JwtProvider : IJwtProvider
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
        var symmSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sAMnUySWiz3dRmVdD6tJINyCwRIVlmAi"));
        var signingCredentials = new SigningCredentials(symmSecurityKey, SecurityAlgorithms.HmacSha256);
        var expiresIn = 30;
       
        var token= new JwtSecurityToken(
            issuer: "SurveyBasketApp",
            audience: "SurveyBasketApp Users",
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expiresIn),
            signingCredentials: signingCredentials
            );

        return (token:new JwtSecurityTokenHandler().WriteToken(token),expiresIn: expiresIn);
    }
}
