using Logic.Services;
using Data.Catalog;
using Data.Implementations;
using Logic.Repositories;
using Data.Enums;
using Data.Factories;

namespace Library.LogicTest
{
    [TestClass]
    public class LibraryServiceTests
    {
        private InMemoryDataContext _context;
        private LibraryService _service;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDataContext();
            var repository = new LibraryRepository(_context);
            var eventService = new EventService(new EventRepository(_context));
            var eventFactory = new EventFactory();
            _service = new LibraryService(repository, eventService, eventFactory);
        }

        [TestMethod]
        public void AddContent_ShouldAddNewItem()
        {
            var book = new Book("Book Title", "Publisher", true, "Author Name", 200, BookGenre.Fantasy);

            bool result = _service.AddContent(book);

            Assert.IsTrue(result, "AddContent should return true.");
        }

        [TestMethod]
        public void RemoveContent_ShouldRemoveItem_WhenItemExists()
        {
            var boardGame = new BoardGame("Game Title", "Publisher", true, 2, 6, 12, GameGenre.Strategy);
            _context.AddItem(boardGame);

            bool result = _service.RemoveContent(boardGame.id);

            Assert.IsTrue(result, "RemoveContent should return true when item exists.");
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
            var book = new Book("Another Book", "Publisher", true, "Author", 150, BookGenre.History);
            _context.AddItem(book);

            var result = _service.GetContent(book.id);

            Assert.IsNotNull(result, "GetContent should not return null.");
            Assert.AreEqual(book.id, result.id, "Returned item should have the correct ID.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no item with such id.")]
        public void GetContent_ShouldThrowException_WhenItemDoesNotExist()
        {
            _service.GetContent(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllContent_ShouldReturnItems_WhenItemsExist()
        {
            _context.AddItem(new Book("Book One", "Publisher", true, "Author", 100, BookGenre.Romance));
            _context.AddItem(new BoardGame("Game One", "Publisher", true, 2, 4, 8, GameGenre.Party));

            var items = _service.GetAllContent();

            Assert.IsNotNull(items, "Returned list should not be null.");
            Assert.AreEqual(2, items.Count, "Should return exactly two items.");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no items in stock.")]
        public void GetAllContent_ShouldThrowException_WhenNoItemsExist()
        {
            _service.GetAllContent();
        }
    }
}
