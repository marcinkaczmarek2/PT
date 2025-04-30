using Logic.Repositories;
using Data.Catalog;
using Data.Implementations;

namespace Repositories.Test
{
    [TestClass]
    public class LibraryRepositoryTest
    {
        private InMemoryDataContext _context;
        private LibraryRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDataContext();
            _repo = new LibraryRepository(_context);
        }

        [TestMethod]
        public void AddContent_ShouldAddItem()
        {
            var book = new Book("Test Book", "TestPublisher", true, "Author Name", 250, BookGenre.Fantasy);

            _repo.AddContent(book);

            List<Borrowable> items = _context.GetItems();
            Assert.AreEqual(1, items.Count, "Should have exactly one item added.");
            Assert.AreEqual(book.id, items[0].id, "Item IDs should match.");
        }

        [TestMethod]
        public void RemoveContent_ShouldRemoveItem()
        {
            var boardGame = new BoardGame("Test Game", "GamePublisher", true, 2, 6, 10, GameGenre.Strategy);
            _context.AddItem(boardGame);

            bool removed = _repo.RemoveContent(boardGame.id);

            Assert.IsTrue(removed, "RemoveContent should return true when item is removed.");
            Assert.AreEqual(0, _context.GetItems().Count, "Context should be empty after removal.");
        }

        [TestMethod]
        public void GetContent_ShouldReturnCorrectItem()
        {
            var book = new Book("Another Book", "PublisherX", true, "Author X", 300, BookGenre.History);
            _context.AddItem(book);

            var retrieved = _repo.GetContent(book.id);

            Assert.IsNotNull(retrieved, "Retrieved item should not be null.");
            Assert.AreEqual(book.id, retrieved.id, "Item IDs should match.");
        }

        [TestMethod]
        public void GetAllContent_ShouldReturnAllItems()
        {
            var book = new Book("Item1", "Publisher1", true, "Author1", 100, BookGenre.Adventure);
            var boardGame = new BoardGame("Item2", "Publisher2", true, 2, 5, 12, GameGenre.Party);

            _context.AddItem(book);
            _context.AddItem(boardGame);

            var items = _repo.GetAllContent();

            Assert.AreEqual(2, items.Count, "Should return exactly two items.");
        }
    }
}
