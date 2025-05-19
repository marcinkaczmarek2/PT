using Data.API;

namespace ServicesTest
{
    internal class MockEvent : IEvent
    {
        public int eventId { get; }
        public int userId { get; }
        public int bookId { get; }

        public MockEvent(int eventId, int userId, int bookId)
        {
            this.eventId = eventId;
            this.userId = userId;
            this.bookId = bookId;
        }
    }
}
