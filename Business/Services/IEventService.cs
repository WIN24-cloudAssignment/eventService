using Business.Models;

namespace Business.Services;

public interface IEventService
{
    Task<EventResult> CreateEventAsync(CreateEventRequest request);
    Task<EventResult<Event?>> GetEventAsync(string eventId);
    Task<EventResult<IEnumerable<Event>>> GetEventsAsync();
}
