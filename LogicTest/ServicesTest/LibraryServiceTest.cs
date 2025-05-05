using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Models;
using Logic.Repositories.Interfaces;
using Logic.Services;
using Logic.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Services.Test
{
    [TestClass]
    public class LibraryServiceTest
    {
        private LibraryService _service;
        private FakeLibraryRepository _libraryRepo;
        private FakeEventService _eventService;
        private FakeEventFactory _eventFactory;

        private Guid _itemId;

        [TestInitialize]
        public void Init()
        {
            _libraryRepo = new FakeLibraryRepository();
            _eventService = new FakeEventService();
            _eventFactory = new FakeEventFactory();

            _service = new LibraryService(_libraryRepo, _eventService, _eventFactory);

            var item = new FakeBorrowable
            {
                Title = "Test Book",
                Publisher = "Test Pub",
                availability = true
            };

            _itemId = item.Id;
            _libraryRepo.AddContent(item);
        }

        [TestMethod]
        public void AddItem_ShouldStoreItem()
        {
            var newItem = new FakeBorrowable
            {
                Title = "New Book",
                Publisher = "New Pub",
                availability = true
            };

            var result = _service.AddContent(newItem);

            Assert.IsTrue(result);
            Assert.AreEqual(2, _libraryRepo.GetAllContent().Count);
            Assert.AreEqual(1, _eventService.Events.Count);
        }

        [TestMethod]
        public void RemoveItem_ShouldRemoveItem()
        {
            var result = _service.RemoveContent(_itemId);

            Assert.IsTrue(result);
            Assert.AreEqual(0, _libraryRepo.GetAllContent().Count);
            Assert.AreEqual(1, _eventService.Events.Count);
        }

        [TestMethod]
        public void GetItem_ShouldReturnCorrectItem()
        {
            var item = _service.GetContent(_itemId);

            Assert.IsNotNull(item);
            Assert.AreEqual(_itemId, item!.Id);
        }

        [TestMethod]
        public void GetAllItems_ShouldReturnAll()
        {
            var result = _service.GetAllContent();
            Assert.AreEqual(1, result.Count);
        }

        // ==== Fake Implementacje ====

        private class FakeBorrowable : IBorrowableL
        {
            public Guid Id { get; } = Guid.NewGuid();
            public string Title { get; set; } = "";
            public string Publisher { get; set; } = "";
            public bool availability { get; set; }
        }

        private class FakeLibraryRepository : ILibraryRepository
        {
            private readonly Dictionary<Guid, IBorrowableL> items = new();

            public void AddContent(IBorrowableL item) => items[item.Id] = item;
            public bool RemoveContent(Guid id) => items.Remove(id);
            public IBorrowableL? GetContent(Guid id) => items.TryGetValue(id, out var i) ? i : null;
            public List<IBorrowableL> GetAllContent() => new(items.Values);
        }

        private class FakeEvent : IEventL
        {
            public Guid EventId { get; } = Guid.NewGuid();
            public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        }

        private class FakeEventService : IEventService
        {
            public List<IEventL> Events { get; } = new();

            public bool AddEvent(IEventL e)
            {
                Events.Add(e);
                return true;
            }

            public List<IEventL> GetAllEvents() => Events;
        }

        private class FakeEventFactory : IEventFactory
        {
            public IEventL CreateItemAddedEvent(Guid itemId, string itemTitle) => new FakeEvent();
            public IEventL CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
            public IEventL CreateItemRemovedEvent(Guid itemId, string itemTitle) => new FakeEvent();
            public IEventL CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
            public IEventL CreateUserAddedEvent(Guid userId, string userEmail) => new FakeEvent();
            public IEventL CreateUserRemovedEvent(Guid userId, string userEmail) => new FakeEvent();
        }
    }
}