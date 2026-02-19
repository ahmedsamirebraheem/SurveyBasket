namespace SurveyBasket.Api.Contracts.Validations;

public class PollRequestValidator : AbstractValidator<PollRequest>
{
    public PollRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .Length(3, 100)
            .WithMessage("Title must be between 3 and 100 characters.");
        RuleFor(x => x.Summary)
            .NotEmpty()
            .WithMessage("Description is required.")
            .Length(1, 1500)
            .WithMessage("Description must be between 1 and 1500 characters.");

        RuleFor(x => x.StartsAt)
         .NotEmpty()
         .WithMessage("Start date is required.")
         .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
         .WithMessage("Start date cannot be in the past.");

        RuleFor(x => x.EndsAt)
            .NotEmpty()
            .WithMessage("End date is required.");

        RuleFor(x=>x)
            .Must(HasValidDates)
            .WithName(nameof(PollRequest.EndsAt))
            .WithMessage("{PropertyName} Should be greater than or equal Start date");

    }

    private bool HasValidDates(PollRequest pollRequest)
    {
        return pollRequest.EndsAt >= pollRequest.StartsAt;
    }
}

