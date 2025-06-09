using Data.API;
using DataTest.DataGenerators;
using Microsoft.Data.SqlClient;

namespace DataTest.Tests
{
    [TestClass]
    [DeploymentItem("Database\\LibraryLINQDatabaseTest.mdf")]
    [DeploymentItem("Database\\LibraryLINQDatabaseTest_log.ldf")]
    public class DataGenerationTest
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

        [TestCleanup]
        public void TestCleanup()
        {
            _repo.Dispose();
            SqlConnection.ClearAllPools();
        }

        [TestMethod]
        public void TestHardcodedDataGenerator()
        {
            IDataGenerator generator = new HardCodedDataGenerator();
            generator.GenerateData(_repo);

            var book = _repo.GetBook(1);
            Assert.IsNotNull(book);
            Assert.AreEqual("Wiedümin", book.title);

            var reader = _repo.GetReader(1);
            Assert.IsNotNull(reader);
            Assert.AreEqual("Jan", reader.name);

            var state = _repo.GetState(1);
            Assert.IsNotNull(state);
            Assert.AreEqual(1, state.quantity);

            var events = _repo.GetAllEvents().ToList();
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(1, events[0].eventId);
        }

        [TestMethod]
        public void TestRandomDataGenerator()
        {
            IDataGenerator generator = new RandomDataGenerator();
            generator.GenerateData(_repo);

            Assert.IsTrue(_repo.GetAllBooks().Any(), "No books were added.");
            Assert.IsTrue(_repo.GetAllReaders().Any(), "No readers were added.");
            Assert.IsTrue(_repo.GetAllStates().Any(), "No states were added.");
            Assert.IsTrue(_repo.GetAllEvents().Any(), "No events were added.");
        }
    }
}
