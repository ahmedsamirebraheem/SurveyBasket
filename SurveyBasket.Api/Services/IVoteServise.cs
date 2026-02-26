using SurveyBasket.Api.Contracts.Votes;

namespace SurveyBasket.Api.Services;

public interface IVoteServise
{
    Task<Result> AddAsync(int pollId,string userId,VoteRequest request,CancellationToken cancellationToken= default);
}
