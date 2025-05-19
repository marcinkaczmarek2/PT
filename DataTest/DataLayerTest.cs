using Data.API;

namespace Data.Test
{
    [TestClass]
    public class DataUnitTest
    {
        private static string testConnectionString;
        private IDataRepository _repo;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            string relativePath = @"Database\LibraryLINQDatabaseTest.mdf";
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string dbPath = Path.Combine(baseDir, relativePath);

            Assert.IsTrue(File.Exists(dbPath), $"Database file not found at: {dbPath}");

            testConnectionString = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={dbPath};Integrated Security=True;Connect Timeout=30;";
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _repo = IDataRepository.CreateNewRepository(testConnectionString);
            _repo.ClearAll();
        }

        [TestMethod]
        public void BookTests()
        {
            _repo.AddBook(1, "Test Book", "Test Publisher", "Test Author", 300, "Fantasy");

            IBook? book = _repo.GetBook(1);
            Assert.IsNotNull(book);
            Assert.AreEqual("Test Book", book.title);
            Assert.AreEqual("Test Publisher", book.publisher);
            Assert.AreEqual("Test Author", book.author);
            Assert.AreEqual(300, book.numberOfPages);
            Assert.AreEqual("Fantasy", book.genre);

            Assert.IsTrue(_repo.GetAllBooks().Any());
            _repo.RemoveBook(1);
            Assert.IsNull(_repo.GetBook(1));
        }

        [TestMethod]
        public void ReaderTests()
        {
            _repo.AddReader(1, "Filip", "Kowalski", "filip@example.com", "123456789", "Student", 0.0m);

            IUser? reader = _repo.GetReader(1);
            Assert.IsNotNull(reader);
            Assert.AreEqual("Filip", reader.name);
            Assert.AreEqual("Kowalski", reader.surname);
            Assert.AreEqual("filip@example.com", reader.email);
            Assert.AreEqual("123456789", reader.phoneNumber);
            Assert.AreEqual("Student", reader.role);

            Assert.IsTrue(_repo.GetAllReaders().Any());
            _repo.RemoveReader(1);
            Assert.IsNull(_repo.GetReader(1));
        }

        [TestMethod]
        public void StateTests()
        {
            _repo.AddBook(1, "Test Book", "Publisher", "Author", 100, "Genre");
            _repo.AddState(1, 5, 1);

            IState? state = _repo.GetState(1);
            Assert.IsNotNull(state);
            Assert.AreEqual(5, state.bookId);
            Assert.AreEqual(1, state.quantity);

            _repo.ChangeQuantity(1, 2);
            Assert.AreEqual(1, _repo.GetState(1)?.quantity);

            Assert.IsTrue(_repo.GetAllStates().Any());
            _repo.RemoveState(1);
            _repo.RemoveBook(1);
            Assert.IsNull(_repo.GetState(1));
        }

        [TestMethod]
        public void EventTests()
        {
            _repo.AddBook(1, "Test Book", "Publisher", "Author", 100, "Genre");
            _repo.AddReader(1, "Anna", "Nowak", "anna@example.com", "987654321", "Student", 0.0m);
            _repo.AddState(1, 5, 1);

            _repo.AddEvent(1, 1, 1);

            var events = _repo.GetAllEvents();
            Assert.AreEqual(1, events.Count());

            IEvent first = events.First();
            Assert.AreEqual(1, first.eventId);
            Assert.AreEqual(1, first.userId);
            Assert.AreEqual(1, first.bookId);

            _repo.RemoveEvent(1);
            _repo.RemoveState(1);
            _repo.RemoveReader(1);
            _repo.RemoveBook(1);

            Assert.AreEqual(0, _repo.GetAllEvents().Count());
        }
    }
}
