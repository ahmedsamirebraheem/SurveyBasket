using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.OutputCaching;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Abstractions.Consts;
using SurveyBasket.Api.Contracts.Votes;
using SurveyBasket.Api.Extensions;
using SurveyBasket.Api.Services;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace SurveyBasket.Api.Controllers;

[Route("api/polls/{pollId}/vote")]
[ApiController]
[Authorize(Roles =DefaultRoles.Member)]
public class VotesController(IQuestionServise questionServise, IVoteServise voteServise) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> Start([FromRoute] int pollId,CancellationToken cancellationToken)
    {
        var userId = "D765FB08-3390-445C-BF80-07BECDB1F816";//User.GetUserId()!;
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
