using Data.Catalog;

namespace Catalog.Tests
{
    [TestClass]
    public class BorrowableTest
    {
        [TestMethod]
        public void BorrowableConstructorTest()
        {
            string title = "Sample Title";
            string publisher = "Sample Publisher";
            bool availability = true;

            var borrowable = new Borrowable(title, publisher, availability);

            Assert.AreEqual(title, borrowable.title);
            Assert.AreEqual(publisher, borrowable.publisher);
            Assert.AreEqual(availability, borrowable.availbility);
            Assert.AreNotEqual(Guid.Empty, borrowable.id);
        }
    }
}
