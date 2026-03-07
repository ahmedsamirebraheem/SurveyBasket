using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using SurveyBasket.Api.Entities;
using SurveyBasket.Api.Helpers;
using SurveyBasket.Api.Persistence;

namespace SurveyBasket.Api.Services;

public class NotificationService(ApplicationDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    IHttpContextAccessor httpContextAccessor,
    IEmailSender emailSender) : INotificationService
{
    public async Task SendNewPollsNotification(int? pollId = null)
    {
        IEnumerable<Poll> polls = [];
        if (pollId.HasValue)
        {
            var poll = await dbContext.Polls.SingleOrDefaultAsync(x => x.Id == pollId && x.IsPublished);
            polls = [poll!];
        }
        else
        {
            polls = await dbContext.Polls
                .Where(x => x.IsPublished && x.StartsAt == DateOnly.FromDateTime(DateTime.UtcNow))
                .AsNoTracking()
                .ToListAsync();
        }
        //to do select members only
        var users = await userManager.Users.ToListAsync();

        var origin = httpContextAccessor.HttpContext?.Request.Headers.Origin;

        foreach (var poll in polls)
        {
            foreach (var user in users)
            {
                var placeHolders = new Dictionary<string, string>
                {
                    { "{{name}}" , user.FirstName},
                    { "{{pollTill}}" , poll.Title},
                    { "{{endDate}}" , poll.EndsAt.ToString()},
                    { "{{url}}" , $"{origin}/polls/start/{poll.Id}"}
                };
                var body = EmailBodyBuilder.GenerateEmailBody("PollNotification", placeHolders);
                await emailSender.SendEmailAsync(user.Email!, $"📢 Survey Basket New Poll - {poll.Title}", body);
            }
        }
    }
}
