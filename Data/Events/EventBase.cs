using Data.API.Models;

namespace Data.Events
{
    internal abstract class EventBase : IEventD
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
