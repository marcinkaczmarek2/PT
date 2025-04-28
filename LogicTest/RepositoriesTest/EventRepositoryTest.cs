using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
using Data.Events;
using Data;
using System.Collections.Generic;

namespace Repositories.Test
{
    [TestClass]
    public class EventRepositoryTest
    {
        [TestMethod]
        public void AddEvent_ShouldCallContext()
        {
            var context = new InMemoryDataContext();
            var repo = new EventRepository(context);
            var eventBase = new TestEvent();

            repo.AddEvent(eventBase);

            Assert.AreEqual(1, context.GetEvents().Count);
            Assert.AreEqual(eventBase.eventId, context.GetEvents()[0].eventId);
        }

        [TestMethod]
        public void GetAllEvents_ShouldReturnAllEvents()
        {
            var context = new InMemoryDataContext();
            var repo = new EventRepository(context);

            var event1 = new TestEvent();
            var event2 = new TestEvent();
            context.AddEvent(event1);
            context.AddEvent(event2);

            var events = repo.GetAllEvents();

            Assert.AreEqual(2, events.Count);
        }

        private class TestEvent : EventBase { }
    }
}
