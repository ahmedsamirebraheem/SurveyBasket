using Microsoft.AspNetCore.Authorization;
using SurveyBasket.Api.Abstractions;
using SurveyBasket.Api.Abstractions.Consts;
using SurveyBasket.Api.Authentication.Filters;
using SurveyBasket.Api.Contracts.Common;
using SurveyBasket.Api.Contracts.Questions;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers;

[Route("api/polls/{pollId}/[controller]")]
[ApiController]
[Authorize]
public class QuestionsController(IQuestionServise questionServise) : ControllerBase
{
    [HttpGet("")]
    [HasPermission(Permissions.GetQuestions)]
    public async Task<IActionResult> GetAll([FromRoute] int pollId, [FromQuery] RequestFilters filters ,CancellationToken cancellationToken)
    {
        var result = await questionServise.GetAllAsync(pollId, filters,cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    
    [HttpGet("{id}")]
    [HasPermission(Permissions.GetQuestions)]
    public async Task<IActionResult> Get([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await questionServise.GetByIdAsync(pollId, id, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : result.ToProblem();
    }
    
    [HttpPost("")]
    [HasPermission(Permissions.AddQuestions)]
    public async Task<IActionResult> Add([FromRoute] int pollId, [FromBody] QuestionRequest request, CancellationToken cancellationToken = default)
    {
        var result = await questionServise.AddAsync(pollId, request, cancellationToken);

        return result.IsSuccess
        ? CreatedAtAction(nameof(Get), new { pollId, result.Value.Id }, result.Value)
        : result.ToProblem();


    }
    
    [HttpPut("{id}")]
    [HasPermission(Permissions.UpdateQuestions)]
    public async Task<IActionResult> Update([FromRoute] int pollId, [FromRoute] int id, [FromBody] QuestionRequest request, CancellationToken cancellationToken = default)
    {
        var result = await questionServise.UpdateAsync(pollId, id, request, cancellationToken);

        return result.IsSuccess
            ?
             NoContent():

         result.ToProblem();


    }
  
    [HttpPut("{id}/toggleStatus")]
    [HasPermission(Permissions.UpdateQuestions)]
    public async Task<IActionResult> ToggleStatus([FromRoute] int pollId, [FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await questionServise.ToggleStatusAsync(pollId, id, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}
