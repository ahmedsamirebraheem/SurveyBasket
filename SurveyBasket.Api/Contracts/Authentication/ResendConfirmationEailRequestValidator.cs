namespace SurveyBasket.Api.Contracts.Authentication;

public class ResendConfirmationEailRequestValidator : AbstractValidator<ResendConfirmationEailRequest>
{
    public ResendConfirmationEailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        ;

    }
}
