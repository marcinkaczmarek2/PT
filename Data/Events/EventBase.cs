
namespace Data.Events
{
    public abstract class EventBase
    {
        public DateTime timestamp { get; set; }
        public Guid eventId { get; private set; }

        protected EventBase()
        {
            eventId = Guid.NewGuid();
            timestamp = DateTime.UtcNow;
        }
    }
}
