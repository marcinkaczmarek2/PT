using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Models;
using Logic.Services;
using Logic.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Services.Test
{
    [TestClass]
    public class EventServiceTest
    {
        private EventService _eventService;

        [TestInitialize]
        public void Initialize()
        {
            _eventService = new EventService();
        }

        [TestMethod]
        public void AddEvent_ShouldAddEventCorrectly()
        {
            var testEvent = new FakeEvent();

            bool result = _eventService.AddEvent(testEvent);

            Assert.IsTrue(result, "AddEvent should return true.");
            Assert.AreEqual(1, _eventService.GetAllEvents().Count);
            Assert.AreEqual(testEvent.EventId, _eventService.GetAllEvents()[0].EventId);
        }

        [TestMethod]
        public void GetAllEvents_ShouldReturnEvents_WhenEventsExist()
        {
            var testEvent = new FakeEvent();
            _eventService.AddEvent(testEvent);

            var result = _eventService.GetAllEvents();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(testEvent.EventId, result[0].EventId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Error, no events found.")]
        public void GetAllEvents_ShouldThrowException_WhenNoEventsExist()
        {
            var service = new EventServiceWithValidation();
            service.GetAllEvents();
        }


        private class FakeEvent : IEventL
        {
            public Guid EventId { get; } = Guid.NewGuid();
            public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        }

        private class EventServiceWithValidation : IEventService
        {
            private readonly List<IEventL> events = new();

            public bool AddEvent(IEventL e)
            {
                events.Add(e);
                return true;
            }

            public List<IEventL> GetAllEvents()
            {
                if (events.Count == 0)
                    throw new InvalidOperationException("Error, no events found.");
                return new List<IEventL>(events);
            }
        }
    }
}
