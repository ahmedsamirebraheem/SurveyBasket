using Microsoft.IdentityModel.Tokens;
using SurveyBasket.Api.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace SurveyBasket.Api.Authentication;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
{
    private readonly JwtOptions _options = options.Value;

    //public (string token, int expiresIn) GenerateToken(ApplicationUser user,
    //    IEnumerable<string> roles,
    //    IEnumerable<string> permissions)
    //{
    //    Claim[] claims = [
    //        new(JwtRegisteredClaimNames.Sub, user.Id),
    //        new(JwtRegisteredClaimNames.Email, user.Email!),
    //        new(JwtRegisteredClaimNames.GivenName, user.FirstName),
    //        new(JwtRegisteredClaimNames.FamilyName, user.LastName),
    //        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    //        new(nameof(roles),JsonSerializer.Serialize(roles),JsonClaimValueTypes.JsonArray),
    //        new(nameof(permissions),JsonSerializer.Serialize(permissions),JsonClaimValueTypes.JsonArray),
    //    ];

    //    var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

    //    var singingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

    //    var token = new JwtSecurityToken(
    //        issuer: _options.Issuer,
    //        audience: _options.Audience,
    //        claims: claims,
    //        expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
    //        signingCredentials: singingCredentials
    //    );

    //    return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: _options.ExpiryMinutes * 60);
    //}

    public (string token, int expiresIn) GenerateToken(ApplicationUser user,
    IEnumerable<string> roles,
    IEnumerable<string> permissions)
    {
        var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Sub, user.Id),
        new(JwtRegisteredClaimNames.Email, user.Email!),
        new(JwtRegisteredClaimNames.GivenName, user.FirstName),
        new(JwtRegisteredClaimNames.FamilyName, user.LastName),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

        // ✅ ضيف كل Role كـ Claim منفصل
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // ✅ ضيف كل Permission كـ Claim منفصل (ده اللي الـ Handler مستنيه)
        foreach (var permission in permissions)
        {
            claims.Add(new Claim("permissions", permission));
        }

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var singingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims, // نمرر الـ List هنا
            expires: DateTime.UtcNow.AddMinutes(_options.ExpiryMinutes),
            signingCredentials: singingCredentials
        );

        return (token: new JwtSecurityTokenHandler().WriteToken(token), expiresIn: _options.ExpiryMinutes * 60);
    }

    public string? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                IssuerSigningKey = symmetricSecurityKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;

            return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
        }
        catch
        {
            return null;
        }
    }

   
}