using Services.API;

namespace ServicesTest
{
    [TestClass]
    public sealed class LibraryServiceUnitTest
    {
        [TestMethod]
        public void BookTests()
        {
            ILibraryService service = ILibraryService.CreateNewService(new MockDataLayer());
            service.AddBook(1, "The Hobbit", "George Allen & Unwin", "J.R.R. Tolkien", 310, "Fantasy").Wait();

            var books = service.GetAllBooks();

            Assert.IsNotNull(books);
            Assert.IsTrue(books.Count > 0);
        }

        [TestMethod]
        public void ReaderTests()
        {
            ILibraryService service = ILibraryService.CreateNewService(new MockDataLayer());
            service.AddReader(1, "Emma", "Johnson", "emma.j@example.com", "555123456", "Reader", 0m).Wait();

            var readers = service.GetAllReaders();

            Assert.IsNotNull(readers);
            Assert.IsTrue(readers.Count > 0);
        }

        [TestMethod]
        public void StateTests()
        {
            ILibraryService service = ILibraryService.CreateNewService(new MockDataLayer());
            service.AddBook(1, "The Hobbit", "George Allen & Unwin", "J.R.R. Tolkien", 310, "Fantasy").Wait();
            service.AddState(1, 4, 1).Wait();

            var states = service.GetAllStates();

            Assert.IsNotNull(states);
            Assert.IsTrue(states.Count > 0);
        }

        [TestMethod]
        public void EventTests()
        {
            ILibraryService service = ILibraryService.CreateNewService(new MockDataLayer());
            service.AddBook(1, "The Hobbit", "George Allen & Unwin", "J.R.R. Tolkien", 310, "Fantasy").Wait();
            service.AddState(1, 4, 1).Wait();
            service.AddReader(1, "Emma", "Johnson", "emma.j@example.com", "555123456", "Reader", 0m).Wait();

            service.BorrowBook(1, 1, 1).Wait();

            var events = service.GetAllEvents();

            Assert.IsNotNull(events);
            Assert.IsTrue(events.Count > 0);
        }
    }
}
