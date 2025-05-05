using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
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
            var item = new FakeBorrowable("Test Item", "Publisher", true);

            _repo.AddContent(item);

            Assert.AreEqual(1, _context.Items.Count);
            Assert.AreEqual(item.Id, _context.Items[0].Id);
        }

        [TestMethod]
        public void RemoveContent_ShouldRemoveItem()
        {
            var item = new FakeBorrowable("Game", "GamePublisher", true);
            _context.Items.Add(item);

            bool removed = _repo.RemoveContent(item.Id);

            Assert.IsTrue(removed);
            Assert.AreEqual(0, _context.Items.Count);
        }

        [TestMethod]
        public void GetContent_ShouldReturnCorrectItem()
        {
            var item = new FakeBorrowable("Another Item", "PublisherX", true);
            _context.Items.Add(item);

            var retrieved = _repo.GetContent(item.Id);

            Assert.IsNotNull(retrieved);
            Assert.AreEqual(item.Id, retrieved.Id);
        }

        [TestMethod]
        public void GetAllContent_ShouldReturnAllItems()
        {
            var item1 = new FakeBorrowable("Item1", "Publisher1", true);
            var item2 = new FakeBorrowable("Item2", "Publisher2", true);
            _context.Items.Add(item1);
            _context.Items.Add(item2);

            var result = _repo.GetAllContent();

            Assert.AreEqual(2, result.Count);
        }

        public interface IFakeBorrowable
        {
            Guid Id { get; }
            string Title { get; }
            string Publisher { get; }
            bool IsAvailable { get; }
        }
        public class FakeBorrowable : IFakeBorrowable
        {
            public Guid Id { get; } = Guid.NewGuid();
            public string Title { get; }
            public string Publisher { get; }
            public bool IsAvailable { get; }

            public FakeBorrowable(string title, string publisher, bool available)
            {
                Title = title;
                Publisher = publisher;
                IsAvailable = available;
            }
        }

        public class FakeDataContext : ILogicData
        {
            public List<IFakeBorrowable> Items { get; } = new();

            public void AddItem(IFakeBorrowable item) => Items.Add(item);

            public bool DeleteItem(Guid id) => Items.RemoveAll(i => i.Id == id) > 0;

            public IFakeBorrowable? GetItem(Guid id) => Items.Find(i => i.Id == id);

            public List<IFakeBorrowable> GetItems() => new List<IFakeBorrowable>(Items);
        }
        
        public interface ILogicData
        {
            void AddItem(IFakeBorrowable item);
            bool DeleteItem(Guid id);
            IFakeBorrowable? GetItem(Guid id);
            List<IFakeBorrowable> GetItems();
        }
        public class LibraryRepository
        {
            private readonly ILogicData context;

            public LibraryRepository(ILogicData context)
            {
                this.context = context;
            }

            public void AddContent(IFakeBorrowable item)
            {
                context.AddItem(item);
            }

            public bool RemoveContent(Guid id)
            {
                return context.DeleteItem(id);
            }

            public IFakeBorrowable? GetContent(Guid id)
            {
                return context.GetItem(id);
            }

            public List<IFakeBorrowable> GetAllContent()
            {
                return context.GetItems();
            }
        }
    }
}
