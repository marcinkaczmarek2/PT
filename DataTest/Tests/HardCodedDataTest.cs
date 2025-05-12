using DataLayerTest.TestDataGeneration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataLayerTest
{
    [TestClass]
    public class HardcodedDataTest : DataTest
    {
        [TestInitialize]
        public override void Initialize()
        {
            IDataGenerator generator = new HardcodedDataGenerator();
            _data = generator.GetData();
        }

        [TestMethod]
        public void TestHardcodedBook()
        {
            Assert.AreEqual(1, _data.GetItems().Count, "Incorrect number of hardcoded books.");
        }

        [TestMethod]
        public void TestHardcodedUser()
        {
            Assert.AreEqual(1, _data.GetUsers().Count, "Incorrect number of hardcoded users.");
        }
    }
}
