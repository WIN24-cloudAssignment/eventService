using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class Event
{
    public string Id { get; set; } = null!;
    public string? Image { get; set; }
    public string? Title { get; set; }
    public DateTime EventDate { get; set; }
    public string? Location { get; set; }
    public string? Description { get; set; }
}
