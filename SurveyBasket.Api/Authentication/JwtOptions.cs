using System.ComponentModel.DataAnnotations;

namespace SurveyBasket.Api.Authentication;

public class JwtOptions
{
    public const string SectionName = "Jwt";
    [Required]
    public string Key { get; init; } = string.Empty;
    [Required]
    public string Issuer { get; init; } = string.Empty;
    [Required]
    public string Audience { get; init; } = string.Empty;
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Expiry Minutes must be greater than or equal 1.")]
    public int ExpiryMinutes { get; init; }
}
