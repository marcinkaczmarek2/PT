using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
using Data.Catalog;
using Data;
using System.Collections.Generic;

namespace Repositories.Test
{
    [TestClass]
    public class LibraryRepositoryTest
    {
        [TestMethod]
        public void AddContent_ShouldAddItem()
        {
            var context = new InMemoryDataContext();
            var repo = new LibraryRepository(context);
            var item = new TestBorrowable("TestTitle", "TestPublisher", true);

            repo.AddContent(item);

            Assert.AreEqual(1, context.GetItems().Count);
        }

        [TestMethod]
        public void RemoveContent_ShouldRemoveItem()
        {
            var context = new InMemoryDataContext();
            var repo = new LibraryRepository(context);
            var item = new TestBorrowable("TestTitle", "TestPublisher", true);
            context.AddItem(item);

            var removed = repo.RemoveContent(item.id);

            Assert.IsTrue(removed);
            Assert.AreEqual(0, context.GetItems().Count);
        }

        [TestMethod]
        public void GetContent_ShouldReturnCorrectItem()
        {
            var context = new InMemoryDataContext();
            var repo = new LibraryRepository(context);
            var item = new TestBorrowable("TestTitle", "TestPublisher", true);
            context.AddItem(item);

            var retrieved = repo.GetContent(item.id);

            Assert.IsNotNull(retrieved);
            Assert.AreEqual(item.id, retrieved.id);
        }

        [TestMethod]
        public void GetAllContent_ShouldReturnAllItems()
        {
            var context = new InMemoryDataContext();
            var repo = new LibraryRepository(context);
            context.AddItem(new TestBorrowable("Item1", "Publisher1", true));
            context.AddItem(new TestBorrowable("Item2", "Publisher2", true));

            var items = repo.GetAllContent();

            Assert.AreEqual(2, items.Count);
        }

        private class TestBorrowable : Borrowable
        {
            public TestBorrowable(string title, string publisher, bool availability)
                : base(title, publisher, availability) { }
        }
    }
}
