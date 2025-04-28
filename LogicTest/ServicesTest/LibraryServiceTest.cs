using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Data.Catalog;
using Data;
using Logic.Repositories;

namespace Library.LogicTest
{
    [TestClass]
    public class LibraryServiceTests
    {
        [TestMethod]
        public void AddContent_ShouldAddNewItem()
        {
            var context = new InMemoryDataContext();
            var repository = new LibraryRepository(context);
            var service = new LibraryService(repository);

            var item = new TestBorrowable("Book", "Publisher", true);

            var result = service.AddContent(item);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveContent_ShouldRemoveItem_WhenItemExists()
        {
            var context = new InMemoryDataContext();
            var repository = new LibraryRepository(context);
            var service = new LibraryService(repository);

            var item = new TestBorrowable("Item1", "Publisher", true);
            context.AddItem(item);

            var result = service.RemoveContent(item.id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveContent_ShouldReturnFalse_WhenItemDoesNotExist()
        {
            var context = new InMemoryDataContext();
            var repository = new LibraryRepository(context);
            var service = new LibraryService(repository);

            var result = service.RemoveContent(Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetContent_ShouldReturnItem_WhenItemExists()
        {
            var context = new InMemoryDataContext();
            var repository = new LibraryRepository(context);
            var service = new LibraryService(repository);

            var item = new TestBorrowable("Item1", "Publisher", true);
            context.AddItem(item);

            var result = service.GetContent(item.id);

            Assert.IsNotNull(result);
            Assert.AreEqual(item.id, result.id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no item with such id.")]
        public void GetContent_ShouldThrowException_WhenItemDoesNotExist()
        {
            var context = new InMemoryDataContext();
            var repository = new LibraryRepository(context);
            var service = new LibraryService(repository);

            service.GetContent(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllContent_ShouldReturnItems_WhenItemsExist()
        {
            var context = new InMemoryDataContext();
            var repository = new LibraryRepository(context);
            var service = new LibraryService(repository);

            context.AddItem(new TestBorrowable("Item1", "Publisher", true));
            context.AddItem(new TestBorrowable("Item2", "Publisher", true));

            var result = service.GetAllContent();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no items in stock.")]
        public void GetAllContent_ShouldThrowException_WhenNoItemsExist()
        {
            var context = new InMemoryDataContext();
            var repository = new LibraryRepository(context);
            var service = new LibraryService(repository);

            service.GetAllContent();
        }

        private class TestBorrowable : Borrowable
        {
            public TestBorrowable(string title, string publisher, bool availability)
                : base(title, publisher, availability) { }
        }
    }
}
