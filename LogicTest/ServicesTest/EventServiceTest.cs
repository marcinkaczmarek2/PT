using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Data.Events;
using Data;
using Logic.Repositories;
using System;
using System.Collections.Generic;

namespace Services.Test
{
    [TestClass]
    public class EventServiceTest
    {
        private InMemoryDataContext _context;
        private EventService _eventService;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDataContext();
            var repository = new EventRepository(_context);
            _eventService = new EventService(repository);
        }

        [TestMethod]
        public void AddEvent_ShouldAddEventCorrectly()
        {
            var eventBase = new ItemAddedEvent(Guid.NewGuid(), "Sample Title");

            bool result = _eventService.AddEvent(eventBase);

            Assert.IsTrue(result, "AddEvent should return true.");

            List<EventBase> events = _context.GetEvents();
            bool found = false;
            foreach (EventBase e in events)
            {
                if (e.eventId == eventBase.eventId)
                {
                    found = true;
                    break;
                }
            }
            Assert.IsTrue(found, "Event should be added to the context.");
        }

        [TestMethod]
        public void GetAllEvents_ShouldReturnEvents_WhenEventsExist()
        {
            var eventBase = new ItemAddedEvent(Guid.NewGuid(), "Another Title");
            _context.AddEvent(eventBase);

            List<EventBase> events = _eventService.GetAllEvents();

            Assert.IsNotNull(events, "Returned event list should not be null.");
            Assert.AreEqual(1, events.Count, "There should be exactly one event.");
            Assert.AreEqual(eventBase.eventId, events[0].eventId, "Event ID should match.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no events found.")]
        public void GetAllEvents_ShouldThrowException_WhenNoEventsExist()
        {
            _eventService.GetAllEvents();
        }
    }
}
