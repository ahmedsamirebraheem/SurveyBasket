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
    public IActionResult GetAll()
    {
        var polls = pollService.GetAll();
        return Ok(polls.Adapt<IEnumerable<PollResponse>>());
    }

    [HttpGet("{id}")]
    public IActionResult Get([FromRoute] int id)
    {
        Poll? poll = pollService.Get(id);
        return poll is null? NotFound(): Ok(poll.Adapt<PollResponse>());
    }

    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest request)
    {   
        
        var newPoll = pollService.Add(request.Adapt<Poll>());
        return CreatedAtAction(nameof(Get),new {id = newPoll.Id}, newPoll);
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromRoute] int id, [FromBody] CreatePollRequest request)
    {
      var isUpdated = pollService.Update(id, request.Adapt<Poll>());
        return isUpdated? NoContent(): NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
       return pollService.Delete(id)? NoContent():NotFound();
    }

}
