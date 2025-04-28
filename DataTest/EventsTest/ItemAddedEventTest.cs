using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Events;

namespace Events.Tests
{
    [TestClass]
    public class ItemAddedEventTest
    {
        [TestMethod]
        public void ItemAddedEventConstructorTest()
        {
            var itemId = Guid.NewGuid();
            var itemTitle = "Chess";

            var itemAddedEvent = new ItemAddedEvent(itemId, itemTitle);

            Assert.AreEqual(itemId, itemAddedEvent.itemId);
            Assert.AreEqual(itemTitle, itemAddedEvent.itemTitle);
        }
    }
}
