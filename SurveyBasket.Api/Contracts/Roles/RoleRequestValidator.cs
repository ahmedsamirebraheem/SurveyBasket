namespace SurveyBasket.Api.Contracts.Roles;

public class RoleRequestValidator: AbstractValidator<RoleRequest>
{
    public RoleRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 256);

        RuleFor(x => x.Permissions)
            .NotNull()
            .NotEmpty();

        RuleFor(x => x.Permissions)
            .Must(x => x.Distinct().Count() == x.Count)
            .WithMessage("you cant add doublicated permission for the same role")
            .When(x => x.Permissions != null);


    }
}
