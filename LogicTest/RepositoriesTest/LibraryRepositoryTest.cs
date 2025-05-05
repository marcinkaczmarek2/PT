using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
using Data.Enums;
using Data.Catalog;
using Data.API.Models;
using Data.API;
using System;
using System.Collections.Generic;

namespace Repositories.Test
{
    [TestClass]
    public class LibraryRepositoryTest
    {
        private FakeDataContext _context;
        private LibraryRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _context = new FakeDataContext();
            _repo = new LibraryRepository(_context);
        }

        [TestMethod]
        public void AddContent_ShouldAddItem()
        {
            var item = new TestBorrowable("Test Item", "Publisher", true);

            _repo.AddContent(item);

            Assert.AreEqual(1, _context.Items.Count, "Should have exactly one item added.");
            Assert.AreEqual(item.id, _context.Items[0].id, "Item IDs should match.");
        }

        [TestMethod]
        public void RemoveContent_ShouldRemoveItem()
        {
            var item = new TestBorrowable("Game", "GamePublisher", true);
            _context.Items.Add(item);

            bool removed = _repo.RemoveContent(item.id);

            Assert.IsTrue(removed, "Should return true when item is removed.");
            Assert.AreEqual(0, _context.Items.Count, "Should be empty after removal.");
        }

        [TestMethod]
        public void GetContent_ShouldReturnCorrectItem()
        {
            var item = new TestBorrowable("Another Item", "PublisherX", true);
            _context.Items.Add(item);

            var retrieved = _repo.GetContent(item.id);

            Assert.IsNotNull(retrieved);
            Assert.AreEqual(item.id, retrieved.id);
        }

        [TestMethod]
        public void GetAllContent_ShouldReturnAllItems()
        {
            var item1 = new TestBorrowable("Item1", "Publisher1", true);
            var item2 = new TestBorrowable("Item2", "Publisher2", true);
            _context.Items.Add(item1);
            _context.Items.Add(item2);

            var result = _repo.GetAllContent();

            Assert.AreEqual(2, result.Count);
        }
        private class FakeDataContext : IData
        {
            public List<IBorrowable> Items { get; } = new();

            public void AddItem(IBorrowable b) => Items.Add(b);

            public bool DeleteItem(Guid id) => Items.RemoveAll(x => x.id == id) > 0;

            public IBorrowable? GetItem(Guid id) => Items.Find(x => x.id == id);

            public List<IBorrowable> GetItems() => Items;

            public List<IUser> GetUsers() => throw new NotImplementedException();
            public IUser? GetUser(Guid id) => throw new NotImplementedException();
            public void AddUser(IUser user) => throw new NotImplementedException();
            public bool DeleteUser(Guid id) => throw new NotImplementedException();
            public List<IEvent> GetEvents() => throw new NotImplementedException();
            public void AddEvent(IEvent eventBase) => throw new NotImplementedException();
        }

        private class TestBorrowable : Borrowable
        {
            public TestBorrowable(string title, string publisher, bool availability)
                : base(title, publisher, availability) { }
        }
    }
}
