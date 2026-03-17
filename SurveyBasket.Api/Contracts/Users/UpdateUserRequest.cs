namespace SurveyBasket.Api.Contracts.Users;

public record UpdateUserRequest(
    string Email,
    IList<string> Roles,
    string FirstName,
    string LastName);
