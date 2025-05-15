using DataTest.TestDataGenerator;

namespace DataTest
{
    [TestClass]
    public class RandomDataTest : DataTest
    {
        [TestInitialize]
        public override void Initialize()
        {
            IDataGenerator generator = new RandomDataGenerator();
            _data = generator.GetData();
        }

        [TestMethod]
        public void TestBooksGenerated()
        {
            Assert.IsTrue(_data.GetItems().Count > 0, "No books generated.");
        }

        [TestMethod]
        public void TestUsersGenerated()
        {
            Assert.IsTrue(_data.GetUsers().Count > 0, "No users generated.");
        }
    }
}
