namespace Data.API.Models
{
    public interface IEventD
    {
        Guid eventId { get; }
        DateTime timestamp { get; set; }
    }
}
