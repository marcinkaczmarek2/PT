using Data.API;

namespace Data.Implementations
{
    internal class Event : IEvent
    {
        public int eventId { get; private set; }
        public int userId { get; private set; }
        public int bookId { get; private set; }

        public Event(int eventId, int userId, int bookId)
        {
            this.eventId = eventId;
            this.userId = userId;
            this.bookId = bookId;
        }
    }
}
