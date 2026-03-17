using SurveyBasket.Api.Abstractions.Consts;

namespace SurveyBasket.Api.Contracts.Users;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
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
            .Must(x=>x.Distinct().Count() == x.Count)
            .WithMessage("You Cannot add doublicated role for the same user")
            .When(x=>x.Roles !=null);

        RuleFor(x => x.Password)
           .NotEmpty()
           .Matches(RegexPatterns.Password)
           .WithMessage("Password should be at least 8 digits and should contains Lowercase, NonAlphanumeric, and Uppercase");

    }
}
