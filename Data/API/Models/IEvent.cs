namespace Data.API.Models
{
    public interface IEvent
    {
        Guid eventId { get; }
        DateTime timestamp { get; set; }
    }
}
