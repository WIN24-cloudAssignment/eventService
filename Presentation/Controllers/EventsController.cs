using Business.Models;
using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(IEventService eventService) : ControllerBase
    {

        private readonly IEventService _eventService = eventService;

        [HttpPost]

        public async Task<IActionResult> Create(CreateEventRequest request)
        {
            if (!ModelState.IsValid) 
                return BadRequest(ModelState);

            var result = await _eventService.CreateEventAsync(request);
            return result.Success ? Ok(result) : StatusCode(500, result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
           var currentEvent = await _eventService.GetEventAsync(id);
            return currentEvent != null ? Ok(currentEvent) : NotFound();
        }

    }
}
