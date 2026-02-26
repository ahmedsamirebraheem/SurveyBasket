using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Votes;
using SurveyBasket.Api.Extensions;
using SurveyBasket.Api.Services;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace SurveyBasket.Api.Controllers;

[Route("api/polls/{pollId}/vote")]
[ApiController]
[Authorize]
public class VotesController(IQuestionServise questionServise, IVoteServise voteServise) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> Start([FromRoute] int pollId,CancellationToken cancellationToken)
    { 
        var userId = User.GetUserId()!;
        var result = await questionServise.GetAvailableAsync(pollId, userId!,cancellationToken);
        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }
        return result.ToProblem();
           
    }
    [HttpPost("")]
    public async Task<IActionResult> Vote([FromRoute] int pollId, [FromBody] VoteRequest request, CancellationToken cancellationToken)
    {
        var userId = User.GetUserId()!;
        var result = await voteServise.AddAsync(pollId, userId!, request, cancellationToken);
        if (result.IsSuccess)
        {
            return Created();
        }
        return result.ToProblem();
    }

}
