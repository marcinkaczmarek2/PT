using Logic.Services;
using Data.API.Models;

[TestClass]
public class EventServiceTest
{
    private class FakeEvent : IEvent
    {
        public Guid eventId { get; } = Guid.NewGuid();
        public DateTime timestamp { get; set; } = DateTime.Now;
    }

    [TestMethod]
    public void AddEvent_ShouldAddEvent()
    {
        var service = new EventService();
        var e = new FakeEvent();

        var result = service.AddEvent(e);
        var allEvents = service.GetAllEvents();

        Assert.IsTrue(result);
        Assert.AreEqual(1, allEvents.Count);
        Assert.AreEqual(e.eventId, allEvents[0].eventId);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void AddEvent_NullEvent_Throws()
    {
        var service = new EventService();
        service.AddEvent(null!);
    }

    [TestMethod]
    public void GetAllEvents_ShouldReturnCopy()
    {
        var service = new EventService();
        var e = new FakeEvent();
        service.AddEvent(e);

        var events1 = service.GetAllEvents();
        var events2 = service.GetAllEvents();

        Assert.AreNotSame(events1, events2);
        Assert.AreEqual(events1.Count, events2.Count);
    }

    [TestMethod]
    public void GetAllEvents_InitiallyEmpty()
    {
        var service = new EventService();
        var result = service.GetAllEvents();

        Assert.AreEqual(0, result.Count);
    }
}
