using ModelViewModel.Model.API;

namespace ModelViewModel.Model.Implementation
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
