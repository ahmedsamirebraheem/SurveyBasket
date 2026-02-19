using Microsoft.AspNetCore.Mvc.ModelBinding;
using SurveyBasket.Api.Contracts.Requests;
using SurveyBasket.Api.Mapping;
using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    
    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var polls = await pollService.GetAllAsync(cancellationToken);
        return Ok(polls.Adapt<IEnumerable<PollResponse>>());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        Poll? poll = await pollService.GetAsync(id,cancellationToken);
        return poll is null? NotFound(): Ok(poll.Adapt<PollResponse>());
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] PollRequest request,CancellationToken cancellationToken)
    {   
        
        var newPoll = await pollService.AddAsync(request.Adapt<Poll>(), cancellationToken);
        return CreatedAtAction(nameof(Get),new {id = newPoll.Id}, newPoll);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PollRequest request, CancellationToken cancellationToken = default)
    {
      var isUpdated = await pollService.UpdateAsync(id, request.Adapt<Poll>(),cancellationToken);
        return isUpdated? NoContent(): NotFound();
    }

    [HttpPut("{id}/togglePublish")]
    public async Task<IActionResult> TogglePublish([FromRoute] int id, CancellationToken cancellationToken = default)
    {
        var isUpdated = await pollService.TogglePublishStatusAsync(id, cancellationToken);
        return isUpdated ? NoContent() : NotFound();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id, CancellationToken cancellationToken = default)
    {
       return await pollService.Delete(id, cancellationToken)? NoContent():NotFound();
    }

}
