using Data.Events;

namespace Events.Tests
{
    [TestClass]
    public class ItemRemovedEventTest
    {
        [TestMethod]
        public void ItemRemovedEventConstructorTest()
        {
            var itemId = Guid.NewGuid();
            var itemTitle = "Scrabble";

            var itemRemovedEvent = new ItemRemovedEvent(itemId, itemTitle);

            Assert.AreEqual(itemId, itemRemovedEvent.itemId);
            Assert.AreEqual(itemTitle, itemRemovedEvent.itemTitle);
        }
    }
}
