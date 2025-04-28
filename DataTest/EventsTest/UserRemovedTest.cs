using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Events;

namespace Events.Tests
{
    [TestClass]
    public class UserRemovedEventTest
    {
        [TestMethod]
        public void UserRemovedEventConstructorTest()
        {
            var userId = Guid.NewGuid();
            var email = "olduser@example.com";

            var userRemovedEvent = new UserRemovedEvent(userId, email);

            Assert.AreEqual(userId, userRemovedEvent.userId);
            Assert.AreEqual(email, userRemovedEvent.userEmail);
        }
    }
}
