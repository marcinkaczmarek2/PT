using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
using Data.API.Models;
using System;
using System.Collections.Generic;
using Data.Events;

namespace Repositories.Test
{
    [TestClass]
    public class EventRepositoryTest
    {
        private FakeDataContext _fakeContext;
        private EventRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _fakeContext = new FakeDataContext();
            _repo = new EventRepository(_fakeContext);
        }

        [TestMethod]
        public void AddEvent_ShouldCallContext()
        {
            var testEvent = new TestEvent();

            _repo.AddEvent(testEvent);

            var events = _fakeContext.StoredEvents;
            Assert.AreEqual(1, events.Count, "Should have exactly one event.");
            Assert.AreEqual(testEvent.eventId, events[0].eventId, "Event IDs should match.");
        }

        [TestMethod]
        public void GetAllEvents_ShouldReturnAllEvents()
        {
            var event1 = new TestEvent();
            var event2 = new TestEvent();

            _fakeContext.StoredEvents.Add(event1);
            _fakeContext.StoredEvents.Add(event2);

            var events = _repo.GetAllEvents();

            Assert.AreEqual(2, events.Count, "Should return exactly two events.");
        }
        private class FakeDataContext : Data.API.IData
        {
            public List<IEvent> StoredEvents { get; } = new();

            public List<IEvent> GetEvents() => StoredEvents;

            public void AddEvent(IEvent e) => StoredEvents.Add(e);

            public void AddUser(Data.API.Models.IUser user) { }
            public void AddItem(Data.API.Models.IBorrowable item) { }
            public bool DeleteUser(Guid id) => false;
            public bool DeleteItem(Guid id) => false;
            public Data.API.Models.IUser? GetUser(Guid id) => null;
            public List<Data.API.Models.IUser> GetUsers() => new();
            public Data.API.Models.IBorrowable? GetItem(Guid id) => null;
            public List<Data.API.Models.IBorrowable> GetItems() => new();
        }
        private class TestEvent : EventBase
        {
            public TestEvent()
            {
                typeof(EventBase).GetProperty("eventId")?.SetValue(this, Guid.NewGuid());
            }
        }
    }
}
