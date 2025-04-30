using Data.Events;

namespace Events.Tests
{
    [TestClass]
    public class ItemBorrowedEventTest
    {
        [TestMethod]
        public void ItemBorrowedEventConstructorTest()
        {
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var itemTitle = "Monopoly";

            var itemBorrowedEvent = new ItemBorrowedEvent(userId, itemId, itemTitle);

            Assert.AreEqual(userId, itemBorrowedEvent.userId);
            Assert.AreEqual(itemId, itemBorrowedEvent.itemId);
            Assert.AreEqual(itemTitle, itemBorrowedEvent.itemTitle);
        }
    }
}
