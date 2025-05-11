using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Logic.Services;
using Logic.Services.Interfaces;
using Logic.Repositories.Interfaces;
using Data.API.Models;

[TestClass]
public class LibraryServiceTest
{
    private class FakeItem : IBorrowable
    {
        public Guid id { get; set; }
        public string title { get; set; } = "";
        public string publisher { get; set; } = "";
        public bool availability { get; set; } = true;
    }

    private class FakeEvent : IEvent
    {
        public Guid eventId { get; } = Guid.NewGuid();
        public DateTime timestamp { get; set; } = DateTime.Now;
    }

    private class FakeLibraryRepo : ILibraryRepository
    {
        private readonly Dictionary<Guid, IBorrowable> items = new();

        public void AddContent(IBorrowable content) => items[content.id] = content;
        public bool RemoveContent(Guid id) => items.Remove(id);
        public IBorrowable? GetContent(Guid id) => items.TryGetValue(id, out var item) ? item : null;
        public List<IBorrowable> GetAllContent() => new(items.Values);
    }

    private class FakeEventService : IEventService
    {
        public List<IEvent> Events { get; } = new();
        public bool AddEvent(IEvent eventBase)
        {
            Events.Add(eventBase);
            return true;
        }
        public List<IEvent> GetAllEvents() => new(Events);
    }

    private class FakeEventFactory : IEventFactory
    {
        public IEvent CreateItemAddedEvent(Guid itemId, string itemTitle) => new FakeEvent();
        public IEvent CreateItemRemovedEvent(Guid itemId, string itemTitle) => new FakeEvent();
        public IEvent CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
        public IEvent CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
        public IEvent CreateUserAddedEvent(Guid userId, string userEmail) => new FakeEvent();
        public IEvent CreateUserRemovedEvent(Guid userId, string userEmail) => new FakeEvent();
    }

    private LibraryService CreateService(out FakeLibraryRepo repo, out FakeEventService events)
    {
        repo = new FakeLibraryRepo();
        events = new FakeEventService();
        return new LibraryService(repo, events, new FakeEventFactory());
    }

    [TestMethod]
    public void AddContent_Success()
    {
        var service = CreateService(out var repo, out var events);
        var item = new FakeItem { id = Guid.NewGuid(), title = "Test" };

        var result = service.AddContent(item);

        Assert.IsTrue(result);
        Assert.AreEqual(item, repo.GetContent(item.id));
        Assert.AreEqual(1, events.GetAllEvents().Count);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void AddContent_DuplicateId_Throws()
    {
        var service = CreateService(out var repo, out _);
        var id = Guid.NewGuid();
        repo.AddContent(new FakeItem { id = id, title = "Old" });

        service.AddContent(new FakeItem { id = id, title = "New" });
    }

    [TestMethod]
    public void RemoveContent_Success()
    {
        var service = CreateService(out var repo, out var events);
        var id = Guid.NewGuid();
        var item = new FakeItem { id = id, title = "Item" };
        repo.AddContent(item);

        var result = service.RemoveContent(id);

        Assert.IsTrue(result);
        Assert.IsNull(repo.GetContent(id));
        Assert.AreEqual(1, events.GetAllEvents().Count);
    }

    [TestMethod]
    public void RemoveContent_NotFound_ReturnsFalse()
    {
        var service = CreateService(out _, out var events);

        var result = service.RemoveContent(Guid.NewGuid());

        Assert.IsFalse(result);
        Assert.AreEqual(0, events.GetAllEvents().Count);
    }

    [TestMethod]
    public void GetContent_ExistingItem_ReturnsItem()
    {
        var service = CreateService(out var repo, out _);
        var item = new FakeItem { id = Guid.NewGuid(), title = "X" };
        repo.AddContent(item);

        var result = service.GetContent(item.id);

        Assert.AreEqual(item, result);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void GetContent_NotFound_Throws()
    {
        var service = CreateService(out _, out _);
        service.GetContent(Guid.NewGuid());
    }

    [TestMethod]
    public void GetAllContent_Success()
    {
        var service = CreateService(out var repo, out _);
        repo.AddContent(new FakeItem { id = Guid.NewGuid(), title = "A" });

        var result = service.GetAllContent();

        Assert.AreEqual(1, result.Count);
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void GetAllContent_Empty_Throws()
    {
        var service = CreateService(out _, out _);
        service.GetAllContent();
    }
}
