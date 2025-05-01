using Logic.Repositories;
using Data.Events;
using Data.Implementations;
using Data.API.Models;

namespace Repositories.Test
{
    [TestClass]
    public class EventRepositoryTest
    {
        private InMemoryDataContext _context;
        private EventRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDataContext();
            _repo = new EventRepository(_context);
        }

        [TestMethod]
        public void AddEvent_ShouldCallContext()
        {
            var itemAddedEvent = new ItemAddedEvent(Guid.NewGuid(), "Sample Title");

            _repo.AddEvent(itemAddedEvent);

            List<IEvent> events = _context.GetEvents();
            Assert.AreEqual(1, events.Count, "Should have exactly one event.");
            Assert.AreEqual(itemAddedEvent.eventId, events[0].eventId, "Event IDs should match.");
        }

        [TestMethod]
        public void GetAllEvents_ShouldReturnAllEvents()
        {
            var event1 = new ItemAddedEvent(Guid.NewGuid(), "Title1");
            var event2 = new ItemBorrowedEvent(Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.ToString("yyyy-MM-dd"));

            _context.AddEvent(event1);
            _context.AddEvent(event2);

            List<IEvent> events = _repo.GetAllEvents();

            Assert.AreEqual(2, events.Count, "Should return exactly two events.");
        }
    }
}
