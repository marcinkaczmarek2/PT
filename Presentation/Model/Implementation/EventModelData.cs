using Presentation.Model.API;

namespace Presentation.Model.Implementation
{
    internal class EventModelData : IEventModelData
    {
        public int eventId { get; }
        public int userId { get; }
        public int bookId { get; }

        public EventModelData(int eventId, int userId, int bookId)
        {
            this.eventId = eventId;
            this.userId = userId;
            this.bookId = bookId;
        }
    }
}
