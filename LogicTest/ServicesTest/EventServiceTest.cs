using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;
using Data.API.Models;
using System;
using System.Collections.Generic;

namespace Services.Test
{
    [TestClass]
    public class EventServiceTest
    {
        private EventService _eventService;
        private FakeEventRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _repo = new FakeEventRepository();
            _eventService = new EventService(_repo);    
        }

        [TestMethod]
        public void AddEvent_ShouldAddEventCorrectly()
        {
            var testEvent = new FakeEvent();

            bool result = _eventService.AddEvent(testEvent);

            Assert.IsTrue(result, "AddEvent should return true.");
            Assert.AreEqual(1, _repo.StoredEvents.Count, "One event should be stored.");
            Assert.AreEqual(testEvent.eventId, _repo.StoredEvents[0].eventId, "Event IDs should match.");
        }

        [TestMethod]
        public void GetAllEvents_ShouldReturnEvents_WhenEventsExist()
        {
            var testEvent = new FakeEvent();
            _repo.StoredEvents.Add(testEvent);

            var result = _eventService.GetAllEvents();

            Assert.AreEqual(1, result.Count, "Should return exactly one event.");
            Assert.AreEqual(testEvent.eventId, result[0].eventId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Error, no events found.")]
        public void GetAllEvents_ShouldThrowException_WhenNoEventsExist()
        {
            _eventService.GetAllEvents();
        }
        private class FakeEventRepository : IEventRepository
        {
            public List<IEvent> StoredEvents { get; } = new();

            public void AddEvent(IEvent e) {

                StoredEvents.Add(e);
            }

            public List<IEvent> GetAllEvents()
            {
                return new List<IEvent>(StoredEvents);
            }
        }

        private class FakeEvent : IEvent
        {
            public Guid eventId { get; } = Guid.NewGuid();
            public DateTime timestamp { get; set; }
        }
    }
}
