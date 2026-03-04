using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Results;

namespace SurveyBasket.Api.Services;

public interface IResultServise
{
    public Task<Result<PollVotesResponse>> GetPollVotesAsync(int pollId, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<VotesPerDayResponse>>> GetVotesPerDayAsync(int pollId, CancellationToken cancellationToken = default);
    public Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int pollId, CancellationToken cancellationToken = default);
}
