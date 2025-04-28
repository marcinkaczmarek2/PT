using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Events;

namespace Events.Tests
{
    [TestClass]
    public class EventBaseTest
    {
        [TestMethod]
        public void EventBaseTests()
        {
            var eventBase = new TestEvent();
            Assert.AreNotEqual(Guid.Empty, eventBase.eventId);
            Assert.IsTrue((DateTime.UtcNow - eventBase.timestamp).TotalSeconds < 5);
        }

        private class TestEvent : EventBase { }
    }
}
