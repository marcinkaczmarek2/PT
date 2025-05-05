using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Repositories.Test
{
    [TestClass]
    public class EventRepositoryTest
    {
        private FakeDataContext _context;
        private EventRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _context = new FakeDataContext();
            _repo = new EventRepository(_context);
        }

        [TestMethod]
        public void AddEvent_ShouldCallContext()
        {
            var testEvent = new FakeEvent();

            _repo.AddEvent(testEvent);

            Assert.AreEqual(1, _context.StoredEvents.Count);
            Assert.AreEqual(testEvent.EventId, _context.StoredEvents[0].EventId);
        }

        [TestMethod]
        public void GetAllEvents_ShouldReturnAllEvents()
        {
            var e1 = new FakeEvent();
            var e2 = new FakeEvent();

            _context.StoredEvents.Add(e1);
            _context.StoredEvents.Add(e2);

            var result = _repo.GetAllEvents();

            Assert.AreEqual(2, result.Count);
        }

        public interface IEventFake
        {
            Guid EventId { get; }
            DateTime Timestamp { get; set; }
        }

        public class FakeEvent : IEventFake
        {
            public Guid EventId { get; } = Guid.NewGuid();
            public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        }

        public interface ILogicEventData
        {
            void AddEvent(IEventFake e);
            List<IEventFake> GetEvents();
        }

        public class FakeDataContext : ILogicEventData
        {
            public List<IEventFake> StoredEvents { get; } = new();
            public void AddEvent(IEventFake e) => StoredEvents.Add(e);
            public List<IEventFake> GetEvents() => StoredEvents;
        }

        public class EventRepository
        {
            private readonly ILogicEventData context;
            public EventRepository(ILogicEventData context)
            {
                this.context = context;
            }

            public void AddEvent(IEventFake e)
            {
                context.AddEvent(e);
            }

            public List<IEventFake> GetAllEvents()
            {
                return context.GetEvents();
            }
        }
    }
}
