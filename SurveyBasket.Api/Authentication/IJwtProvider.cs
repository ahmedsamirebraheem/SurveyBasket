namespace SurveyBasket.Api.Authentication;

public interface IJwtProvider
{
    (string Token, int ExpiresIn) GenerateToken(ApplicationUser user);
}
