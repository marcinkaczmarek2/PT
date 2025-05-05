using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Logic.Services.Interfaces;
using Logic.Repositories.Interfaces;
using Data.API.Models;
using System;
using System.Collections.Generic;

namespace Services.Test
{
    [TestClass]
    public class LibraryServiceTests
    {
        private ILibraryRepository _libraryRepository;
        private IEventService _eventService;
        private LibraryService _service;

        private List<IBorrowable> _storage;

        [TestInitialize]
        public void Initialize()
        {
            _storage = new List<IBorrowable>();
            _libraryRepository = new FakeLibraryRepository(_storage);
            _eventService = new FakeEventService();

            _service = new LibraryService(_libraryRepository, _eventService, null!); 
        }
        
        [TestMethod]
        public void RemoveContent_ShouldReturnFalse_WhenItemDoesNotExist()
        {
            bool result = _service.RemoveContent(Guid.NewGuid());

            Assert.IsFalse(result, "RemoveContent should return false when item does not exist.");
        }

        [TestMethod]
        public void GetContent_ShouldReturnItem_WhenItemExists()
        {
            var item = new TestBorrowable("Gettable Item", "Publisher", true);
            _storage.Add(item);

            var result = _service.GetContent(item.id);

            Assert.IsNotNull(result, "GetContent should not return null.");
            Assert.AreEqual(item.id, result.id, "Returned item should have the correct ID.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetContent_ShouldThrowException_WhenItemDoesNotExist()
        {
            _service.GetContent(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllContent_ShouldReturnItems_WhenItemsExist()
        {
            _storage.Add(new TestBorrowable("Item 1", "Publisher 1", true));
            _storage.Add(new TestBorrowable("Item 2", "Publisher 2", true));

            var items = _service.GetAllContent();

            Assert.IsNotNull(items, "Returned list should not be null.");
            Assert.AreEqual(2, items.Count, "Should return exactly two items.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAllContent_ShouldThrowException_WhenNoItemsExist()
        {
            _service.GetAllContent();
        }

        private class TestBorrowable : IBorrowable
        {
            public Guid id { get; } = Guid.NewGuid();
            public string title { get; set; }
            public string publisher { get; set; }
            public bool availability { get; set; }

            public TestBorrowable(string title, string publisher, bool availability)
            {
                this.title = title;
                this.publisher = publisher;
                this.availability = availability;
            }
        }

        private class FakeLibraryRepository : ILibraryRepository
        {
            private readonly List<IBorrowable> _items;

            public FakeLibraryRepository(List<IBorrowable> items)
            {
                _items = items;
            }

            public void AddContent(IBorrowable item)
            {
                _items.Add(item);
            }

            public bool RemoveContent(Guid id)
            {
                var item = GetContent(id);
                if (item == null) return false;
                _items.Remove(item);
                return true;
            }

            public IBorrowable? GetContent(Guid id)
            {
                return _items.Find(i => i.id == id);
            }

            public List<IBorrowable> GetAllContent()
            {
                return _items;
            }
        }

        private class FakeEventService : IEventService
        {
            public bool AddEvent(IEvent eventBase) => true;

            public List<IEvent> GetAllEvents() => new List<IEvent>();
        }
    }
}
