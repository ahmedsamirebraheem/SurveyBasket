using SurveyBasket.Api.Abstractions;

namespace SurveyBasket.Api.Errors;

public record QuestionErrors
{
    public static readonly Error QuestionNotFound =
        new("Question.NotFound", "No question was found with the given ID",StatusCodes.Status400BadRequest);
    public static readonly Error DuplicatedPollContent =
       new("Question.DuplicatedContent", "Another question with the same content exists", StatusCodes.Status409Conflict);
}