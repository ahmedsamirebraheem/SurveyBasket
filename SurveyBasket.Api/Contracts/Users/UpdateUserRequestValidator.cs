using SurveyBasket.Api.Abstractions.Consts;

namespace SurveyBasket.Api.Contracts.Users;

public class UpdateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress()
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .Length(2, 100)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .Length(2, 100)
            .NotEmpty();

        RuleFor(x => x.Roles)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Roles)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("You Cannot add doublicated role for the same user")
            .When(x => x.Roles != null);

        
    }
}
