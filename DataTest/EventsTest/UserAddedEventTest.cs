using Data.Events;

namespace Events.Tests
{
    [TestClass]
    public class UserAddedEventTest
    {
        [TestMethod]
        public void UserAddedEventConstructorTesty()
        {
            var userId = Guid.NewGuid();
            var email = "user@example.com";

            var userAddedEvent = new UserAddedEvent(userId, email);

            Assert.AreEqual(userId, userAddedEvent.userId);
            Assert.AreEqual(email, userAddedEvent.userEmail);
        }
    }
}
