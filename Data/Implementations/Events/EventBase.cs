using Data.API.Models;

namespace Data.Implementations.Events
{
    internal abstract class EventBase : IEvent
    {
        public DateTime timestamp { get; set; }
        public Guid eventId { get; private set; }

        internal EventBase()
        {
            eventId = Guid.NewGuid();
            timestamp = DateTime.UtcNow;
        }
    }
}
