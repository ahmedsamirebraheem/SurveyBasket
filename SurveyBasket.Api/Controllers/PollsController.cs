using SurveyBasket.Api.Services;

namespace SurveyBasket.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PollsController(IPollService pollService) : ControllerBase
{
    
    [HttpGet("")]
    public IActionResult GetAll()
    {
        return Ok(pollService.GetAll());
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        Poll? poll = pollService.Get(id);
        return poll is null? NotFound(): Ok(poll);
    }

    [HttpPost("")]
    public IActionResult Add(Poll request)
    {   
        var newPoll = pollService.Add(request);
        return CreatedAtAction(nameof(Get),new {id = newPoll.Id}, newPoll);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Poll request)
    {
      var isUpdated = pollService.Update(id, request);
        return isUpdated? NoContent(): NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
       return pollService.Delete(id)? NoContent():NotFound();
    }

}
