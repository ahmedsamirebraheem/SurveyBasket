namespace SurveyBasket.Api.Contracts.Results;

public record PollVotesResponse
(string Title,
    string Summary,
    DateOnly StartsAt,
    DateOnly EndsAt,
    IEnumerable<VoteResponse> Votes);
