using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Errors;

public record UserErrors
{
    public static readonly Error InvalidCredentials = 
        new("User.InvalidCredentials", "Invalid email/password",StatusCodes.Status401Unauthorized);
   
    public static readonly Error DisabledUser =
        new("User.DisabledUser", "This user is Disabled User, please contact your Admin", StatusCodes.Status401Unauthorized);

    public static readonly Error LockedUser =
      new("User.LockedUser", "This user is LockedOut , please contact your Admin", StatusCodes.Status401Unauthorized);


    public static readonly Error InvalidJwtToken =
        new("User.InvalidJwtToken", "Invalid Jwt token",StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidRefreshToken =
        new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);
    
    public static readonly Error DuplicatedEmail =
        new("User.DuplicatedEmail", "Another user with the same email is already exists", StatusCodes.Status409Conflict);

    public static readonly Error EmailNotConfirmed =
      new("User.EmailNotConfirmed", "Email is not confirmed", StatusCodes.Status401Unauthorized);

    public static readonly Error InvalidCode =
  new("User.InvalidCode", "Invalid Code", StatusCodes.Status401Unauthorized);

    public static readonly Error DuplicatedConfirmation =
       new("User.DuplicatedConfirmation", "This email is already confirmed", StatusCodes.Status400BadRequest);

    public static readonly Error UserNotFound =
        new("User.UserNotFound", "User Is Not Found", StatusCodes.Status404NotFound);
    
    public static readonly Error InvalidRoles =
            new("User.InvalidRoles", "Invalid Roles", StatusCodes.Status400BadRequest);

}