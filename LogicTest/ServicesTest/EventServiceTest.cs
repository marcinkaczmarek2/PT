using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Data.Events;
using Logic.Repositories;
using Moq;
using System.Collections.Generic;
using Data.Catalog;
using Data.Users;
using Data;

namespace Services.Test
{
    [TestClass]
    public class EventServiceTest
    {
        [TestMethod]
        public void AddEvent_ShouldCallRepository()
        {
            var repoMock = new Mock<EventRepository>(new object[] { new TestContext() }) { CallBase = true };

            var service = new EventService(repoMock.Object);
            var eventBase = new TestEvent();

            var result = service.AddEvent(eventBase);

            Assert.IsTrue(result);
        }

        private class TestEvent : EventBase { }
        private class TestContext : IData
        {
            public List<EventBase> GetEvents() => new List<EventBase>();
            public void AddEvent(EventBase e) { }
            public void AddItem(Borrowable b) { }
            public void AddUser(User u) { }
            public bool DeleteItem(Guid id) => true;
            public bool DeleteUser(Guid id) => true;
            public Borrowable? GetItem(Guid id) => null;
            public List<Borrowable> GetItems() => new List<Borrowable>();
            public User? GetUser(Guid id) => null;
            public List<User> GetUsers() => new List<User>();
        }
        [TestMethod]
        public void GetAllEvents_ShouldReturnEvents_WhenEventsExist()
        {
            var context = new InMemoryDataContext();
            var repository = new EventRepository(context);
            var service = new EventService(repository);

            var eventBase = new TestEvent();
            context.AddEvent(eventBase);

            var result = service.GetAllEvents();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(eventBase.eventId, result[0].eventId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no events found.")]
        public void GetAllEvents_ShouldThrowException_WhenNoEventsExist()
        {
            var context = new InMemoryDataContext();
            var repository = new EventRepository(context);
            var service = new EventService(repository);

            service.GetAllEvents(); // powinno rzucić wyjątek
        }
    }
}
