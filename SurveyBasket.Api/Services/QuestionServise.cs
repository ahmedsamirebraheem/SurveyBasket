using SurveyBasket.Api.Contracts.Answers;
using SurveyBasket.Api.Contracts.Questions;
using SurveyBasket.Api.Entities;

namespace SurveyBasket.Api.Services;

public class QuestionServise(ApplicationDbContext dbContext) : IQuestionServise
{
    public async Task<Result<IEnumerable<QuestionResponse>>> GetAllAsync(int pollId, CancellationToken cancellationToken = default)
    {
        var pollIsExists = await dbContext.Polls.AnyAsync(x => x.Id == pollId, cancellationToken);
        if (!pollIsExists)
        {
            return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.PollNotFound);
        }
        var questions = await dbContext.Questions
            .Where(x => pollId == x.PollId)
            .Include(x => x.Answers)
            //.Select(q=>new QuestionResponse
            //(
            //    q.Id,
            //    q.Content,
            //    q.Answers.Select(a=>new AnswerResponse(a.Id,a.Content))
            //))
            .ProjectToType<QuestionResponse>()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return Result.Success<IEnumerable<QuestionResponse>>(questions);

    }
    public async Task<Result<IEnumerable<QuestionResponse>>> GetAvailableAsync(int pollId, string userId, CancellationToken cancellationToken = default)
    {
        var pollExists = await dbContext.Polls.AnyAsync(
           x => x.Id == pollId
           && x.IsPublished
           && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow)
           && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow), cancellationToken);

        if (!pollExists)
        {
            return Result.Failure<IEnumerable<QuestionResponse>>(PollErrors.PollNotFound);
        }
        var hasVote = await dbContext
            .Votes
            .AnyAsync(x => x.PollId == pollId && x.UserId == userId,cancellationToken);
        if (hasVote)
        {
            return Result.Failure<IEnumerable<QuestionResponse>>(VoteErrors.DuplicatedVote);
        }
       

       

        var questions = await dbContext
            .Questions
            .Where(x => x.PollId == pollId && x.IsActive)
            .Include(x => x.Answers)
            .Select(q => new QuestionResponse(
                q.Id,
                q.Content,
                q.Answers
                .Where(a => a.IsActive)
                .Select(a=>new AnswerResponse(a.Id,a.Content))
                ))
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return Result.Success<IEnumerable<QuestionResponse>>(questions);
    }
    public async Task<Result<QuestionResponse>> GetByIdAsync(int pollId, int id, CancellationToken cancellationToken = default)
    {
        var question = await dbContext.Questions
            .Where(x => pollId == x.PollId && x.Id == id)
            .Include(x => x.Answers)
            .ProjectToType<QuestionResponse>()
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (question is null)
        {
            return Result.Failure<QuestionResponse>(QuestionErrors.QuestionNotFound);
        }
        return Result.Success(question);
    }
    public async Task<Result<QuestionResponse>> AddAsync(int pollId, QuestionRequest request, CancellationToken cancellationToken = default)
    {
        var pollIsExists = await dbContext.Polls.AnyAsync(x => x.Id == pollId);
        if (!pollIsExists)
        {
            return Result.Failure<QuestionResponse>(PollErrors.PollNotFound);
        }

        var questionIsExists = await dbContext.Questions.AnyAsync(x => x.Content == request.Content && x.PollId == pollId, cancellationToken);
        if (questionIsExists)
        {
            return Result.Failure<QuestionResponse>(QuestionErrors.DuplicatedPollContent);
        }
        var question = request.Adapt<Question>();
        question.PollId = pollId;

        await dbContext.Questions.AddAsync(question, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success(question.Adapt<QuestionResponse>());
    }
    public async Task<Result> UpdateAsync(int pollId, int id, QuestionRequest request, CancellationToken cancellationToken = default)
    {
        var questionIsExists = await dbContext.Questions
             .AnyAsync(x => x.PollId == pollId
                                 && x.Id != id && x.Content == request.Content,
                                 cancellationToken);
        if (questionIsExists)
        {
            return Result.Failure(QuestionErrors.DuplicatedPollContent);
        }
        var question = await dbContext.Questions
            .Include(x=>x.Answers)
            .SingleOrDefaultAsync(x => x.PollId == pollId && x.Id == id, cancellationToken);

        if (question is null)
        {
            return Result.Failure(QuestionErrors.QuestionNotFound);
        }
        question.Content = request.Content;
        var currentAnswers = question.Answers.Select(x => x.Content).ToList();
        var newAnswers = request.Answers.Except(currentAnswers).ToList();
        newAnswers.ForEach(answer =>
        {
            question.Answers.Add(new Answer { Content = answer });
        });
        question.Answers.ToList().ForEach(answer =>
        {
            answer.IsActive = request.Answers.Contains(answer.Content);

        });
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result.Success();
    }

    public async Task<Result> ToggleStatusAsync(int pollId, int id, CancellationToken cancellationToken = default)
    {
        var question = await dbContext.Questions.SingleOrDefaultAsync(x => x.PollId == pollId && x.Id == id, cancellationToken);

        if (question is null)
            return Result.Failure(QuestionErrors.QuestionNotFound);

        question.IsActive = !question.IsActive;

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}
