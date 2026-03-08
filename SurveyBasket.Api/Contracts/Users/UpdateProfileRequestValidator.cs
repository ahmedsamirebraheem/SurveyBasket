using SurveyBasket.Api.Abstractions.Consts;

namespace SurveyBasket.Api.Contracts.Users;

public class UpdateProfileRequestValidator
: AbstractValidator<UpdateProfileRequest>
{
    public UpdateProfileRequestValidator()
    {
        
        RuleFor(x => x.FirstName)
            .Length(2, 100)
            .NotEmpty();
        RuleFor(x => x.LastName)
            .Length(2, 100)
            .NotEmpty();
    }
}
