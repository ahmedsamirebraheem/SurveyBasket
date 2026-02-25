using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Contracts.Questions;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers;

[Route("api/polls/{pollId}/[controller]")]
[ApiController]
[Authorize]
public class QuestionsController(IQuestionServise questionServise) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int pollId,[FromRoute]int id, CancellationToken cancellationToken)
    {
        var result = await questionServise.GetByIdAsync(pollId, id, cancellationToken);
        return result.IsSuccess? Ok(result.Value):result.ToProblem();
    }
    [HttpGet("")]
    public async Task<IActionResult> GetAll([FromRoute] int pollId,CancellationToken cancellationToken)
    {
        var result = await questionServise.GetAllAsync(pollId, cancellationToken);
        return result.IsSuccess? Ok(result.Value):result.ToProblem();
    }
    [HttpPost("")]
    public async Task<IActionResult> Add([FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellationToken = default)
    {
        var result = await questionServise.AddAsync(pollId, request, cancellationToken);

        if (result.IsSuccess)
            return CreatedAtAction(nameof(Get), new {pollId,result.Value.Id},result.Value);

        return result.ToProblem();
            

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int pollId, [FromRoute] int id, [FromBody] QuestionRequest request, CancellationToken cancellationToken = default)
    {
        var result = await questionServise.UpdateAsync(pollId,id, request, cancellationToken);

        if (result.IsSuccess)
            return NoContent();

        return result.ToProblem();


    }
    [HttpPut("{id}/toggleStatus")]
    public async Task<IActionResult> ToggleStatus([FromRoute] int pollId,[FromRoute]int id, CancellationToken cancellationToken)
    {
        var result = await questionServise.ToggleStatusAsync(pollId,id, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
