using Data.Events;

namespace Events.Tests
{
    [TestClass]
    public class ItemReturnedEventTest
    {
        [TestMethod]
        public void ItemReturnedEventConstructorTest()
        {
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            var itemTitle = "Cluedo";

            var itemReturnedEvent = new ItemReturnedEvent(userId, itemId, itemTitle);

            Assert.AreEqual(userId, itemReturnedEvent.userId);
            Assert.AreEqual(itemId, itemReturnedEvent.itemId);
            Assert.AreEqual(itemTitle, itemReturnedEvent.itemTitle);
        }
    }
}
