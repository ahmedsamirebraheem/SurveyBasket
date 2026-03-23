using Azure.Core;
using Hangfire;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Polls;
using SurveyBasket.Api.Entities;
using SurveyBasket.Api.Errors;
using SurveyBasket.Api.Persistence;

namespace SurveyBasket.Api.Services;

public class PollService(ApplicationDbContext context,INotificationService notificationService) : IPollService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<IEnumerable<PollResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var polls = await _context.Polls.AsNoTracking().ToListAsync(cancellationToken);
        return polls.Adapt<IEnumerable<PollResponse>>();
    }

    public async Task<IEnumerable<PollResponse>> GetCurrentAsyncV1(CancellationToken cancellationToken = default)
    {
        var polls = await _context.Polls
            .AsNoTracking()
            .Where(
            x=>x.IsPublished 
            && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) 
            && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
            .ToListAsync(cancellationToken);

        return polls.Adapt<IEnumerable<PollResponse>>();
    }
    public async Task<IEnumerable<PollResponseV2>> GetCurrentAsyncV2(CancellationToken cancellationToken = default)
    {
        var polls = await _context.Polls
            .AsNoTracking()
            .Where(
            x=>x.IsPublished 
            && x.StartsAt <= DateOnly.FromDateTime(DateTime.UtcNow) 
            && x.EndsAt >= DateOnly.FromDateTime(DateTime.UtcNow))
            .ToListAsync(cancellationToken);

        return polls.Adapt<IEnumerable<PollResponseV2>>();
    }

    public async Task<Result<PollResponse>> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);

        return poll is not null
            ? Result.Success(poll.Adapt<PollResponse>())
            : Result.Failure<PollResponse>(PollErrors.PollNotFound);
    }

    public async Task<Result<PollResponse>> AddAsync(PollRequest request, CancellationToken cancellationToken = default)
    {
        var isExisting = await _context.Polls.AnyAsync(p => p.Title == request.Title, cancellationToken);
        if (isExisting)
           return Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);

        var poll = request.Adapt<Poll>();

        await _context.AddAsync(poll, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success(poll.Adapt<PollResponse>());
    }

    public async Task<Result> UpdateAsync(int id, PollRequest request, CancellationToken cancellationToken = default)
    {
        var isExisting = await _context.Polls.AnyAsync(p => p.Title == request.Title && p.Id != id, cancellationToken);
        if (isExisting)
            return Result.Failure<PollResponse>(PollErrors.DuplicatedPollTitle);
        var currentPoll = await _context.Polls.FindAsync(id, cancellationToken);


        if (currentPoll is null)
            return Result.Failure(PollErrors.PollNotFound);

        currentPoll = request.Adapt(currentPoll);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);

        if (poll is null)
            return Result.Failure(PollErrors.PollNotFound);

        _context.Remove(poll);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    public async Task<Result> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await _context.Polls.FindAsync(id, cancellationToken);

        if (poll is null)
            return Result.Failure(PollErrors.PollNotFound);

        poll.IsPublished = !poll.IsPublished;

        await _context.SaveChangesAsync(cancellationToken);

        if (poll.IsPublished && poll.StartsAt == DateOnly.FromDateTime(DateTime.UtcNow))
            BackgroundJob.Enqueue(()=>notificationService.SendNewPollsNotification(poll.Id));

        return Result.Success();
    }
}