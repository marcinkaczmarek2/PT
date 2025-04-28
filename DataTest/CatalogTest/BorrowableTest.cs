using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Catalog;

namespace Library.Tests
{
    [TestClass]
    public class BorrowableTests
    {
        [TestMethod]
        public void Borrowable_Constructor_SetsPropertiesCorrectly()
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
