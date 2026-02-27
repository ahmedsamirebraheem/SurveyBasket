using SurveyBasket.Api.Contracts.Results;

namespace SurveyBasket.Api.Services;

public class ResultServise(ApplicationDbContext dbContext): IResultServise
{
    public async Task<Result<PollVotesResponse>> GetPollVotesAsync(int pollId, CancellationToken cancellationToken = default)
    {
        var pollVotes = await dbContext.Polls
            .Where(p => p.Id == pollId)
            .Select(p => new PollVotesResponse(
            p.Title,
            p.Summary,
            p.StartsAt,
            p.EndsAt,
            p.Votes.Select(v => new VoteResponse(
                $"{v.User.FirstName} {v.User.LastName}",
                v.SubmittedOn,
                v.VoteAnswers.Select(a => new QuestionAnswerResponse(
                    a.Question.Content,
                    a.Answer.Content
                    ))
                ))
            )).SingleOrDefaultAsync(cancellationToken);

        return pollVotes is null
            ? Result.Failure<PollVotesResponse>(PollErrors.PollNotFound)
            : Result.Success(pollVotes);
    }

    public async Task<Result<IEnumerable<VotesPerDayResponse>>> GetVotesPerDayAsync(int pollId, CancellationToken cancellationToken = default)
    {
        var pollExists = await dbContext.Polls.AnyAsync(x => x.Id == pollId, cancellationToken);
        if (!pollExists)
            return Result.Failure<IEnumerable<VotesPerDayResponse>>(PollErrors.PollNotFound);

        var votesPerDay = await dbContext.Votes
            .Where(x => x.PollId == pollId)
            .GroupBy(x => new { Date = DateOnly.FromDateTime(x.SubmittedOn) })
            .Select(g => new VotesPerDayResponse(
                g.Key.Date,
                g.Count()
                ))
            .ToListAsync(cancellationToken);
        return Result.Success<IEnumerable<VotesPerDayResponse>>(votesPerDay);
        
    }
    public async Task<Result<IEnumerable<VotesPerQuestionResponse>>> GetVotesPerQuestionAsync(int pollId, CancellationToken cancellationToken = default)
    {
        var pollExists = await dbContext.Polls.AnyAsync(x => x.Id == pollId, cancellationToken);
        if (!pollExists)
            return Result.Failure<IEnumerable<VotesPerQuestionResponse>>(PollErrors.PollNotFound);

        var votesPerQuestion = await dbContext.VoteAnswers
            .Where(x => x.Vote.PollId == pollId)
            .Select(x => new VotesPerQuestionResponse(
                x.Question.Content,
                x.Question
                .Votes
                .GroupBy(x => new { AnswersId = x.Answer.Id, AnserContent = x.Answer.Content })
                .Select(g => new VotesPerAnswerResponse(
                    g.Key.AnserContent,
                    g.Count()
                    )))).ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<VotesPerQuestionResponse>>(votesPerQuestion);
    }
}
