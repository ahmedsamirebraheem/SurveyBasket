using SurveyBasket.Api.Persistance;

namespace SurveyBasket.Api.Services;

public class PollService(ApplicationDbContext dbContext) : IPollService
{
  
    public async Task<IEnumerable<Poll>> GetAllAsync(CancellationToken cancellationToken = default) => await dbContext.Polls.AsNoTracking().ToListAsync();
    
    public async Task<Poll?> GetAsync(int id, CancellationToken cancellationToken = default) => await dbContext.Polls.FindAsync( id);

    public async Task<Poll> AddAsync(Poll poll, CancellationToken cancellationToken = default)
    {
      await  dbContext.Polls.AddAsync(poll,cancellationToken);
       await dbContext.SaveChangesAsync(cancellationToken);
        return poll;
    }

    public async Task<bool> UpdateAsync(int id, Poll poll, CancellationToken cancellationToken = default)
    {
        var currentPoll = await GetAsync(id, cancellationToken);
        if (currentPoll == null) return false;
        currentPoll.Title = poll.Title;
        currentPoll.Summary = poll.Summary;
        currentPoll.StartsAt = poll.StartsAt;
        currentPoll.EndsAt = poll.EndsAt;
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var poll = await GetAsync(id,cancellationToken);
        if (poll == null) return false;
        dbContext.Remove(poll);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
    public async Task<bool> TogglePublishStatusAsync(int id, CancellationToken cancellationToken = default)
    {
        var poll = await GetAsync(id, cancellationToken);
        if (poll == null) return false;
        poll.IsPublished = !poll.IsPublished;
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

}
