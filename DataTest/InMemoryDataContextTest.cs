using Data.Implementations;
using Data.Users;
using Data.Catalog;
using Data.Events;

namespace Memory.Tests
{
    [TestClass]
    public class InMemoryDataContextTest
    {
        [TestMethod]
        public void AddUserTest()
        {
            var context = new InMemoryDataContext();
            var user = new TestUser("John", "Doe", "john@example.com", "123456789", UserRole.Reader);

            context.AddUser(user);

            var retrievedUser = context.GetUser(user.id);
            Assert.IsNotNull(retrievedUser);
            Assert.AreEqual(user.id, retrievedUser.id);
        }

        [TestMethod]
        public void DeleteUserTest()
        {
            var context = new InMemoryDataContext();
            var user = new TestUser("Jane", "Smith", "jane@example.com", "987654321", UserRole.Reader);

            context.AddUser(user);
            var deleted = context.DeleteUser(user.id);

            Assert.IsTrue(deleted);
            Assert.IsNull(context.GetUser(user.id));
        }

        [TestMethod]
        public void AddItemTest()
        {
            var context = new InMemoryDataContext();
            var item = new TestBorrowable("Game", "Publisher", true);

            context.AddItem(item);

            var retrievedItem = context.GetItem(item.id);
            Assert.IsNotNull(retrievedItem);
            Assert.AreEqual(item.id, retrievedItem.id);
        }

        [TestMethod]
        public void DeleteItemTest()
        {
            var context = new InMemoryDataContext();
            var item = new TestBorrowable("Book", "Publisher", true);

            context.AddItem(item);
            var deleted = context.DeleteItem(item.id);

            Assert.IsTrue(deleted);
            Assert.IsNull(context.GetItem(item.id));
        }

        [TestMethod]
        public void AddEventTest()
        {
            var context = new InMemoryDataContext();
            var eventBase = new TestEvent();

            context.AddEvent(eventBase);

            var events = context.GetEvents();
            Assert.AreEqual(1, events.Count);
            Assert.AreEqual(eventBase.eventId, events.First().eventId);
        }

        [TestMethod]
        public void GetUsersTest()
        {
            var context = new InMemoryDataContext();
            var user1 = new TestUser("User1", "Last1", "user1@example.com", "123", UserRole.Reader);
            var user2 = new TestUser("User2", "Last2", "user2@example.com", "456", UserRole.Admin);

            context.AddUser(user1);
            context.AddUser(user2);

            var users = context.GetUsers();

            Assert.AreEqual(2, users.Count);
            Assert.IsTrue(users.Any(u => u.id == user1.id));
            Assert.IsTrue(users.Any(u => u.id == user2.id));
        }

        [TestMethod]
        public void GetItemsTest()
        {
            var context = new InMemoryDataContext();
            var item1 = new TestBorrowable("Item1", "Publisher1", true);
            var item2 = new TestBorrowable("Item2", "Publisher2", false);

            context.AddItem(item1);
            context.AddItem(item2);

            var items = context.GetItems();

            Assert.AreEqual(2, items.Count);
            Assert.IsTrue(items.Any(i => i.id == item1.id));
            Assert.IsTrue(items.Any(i => i.id == item2.id));
        }
        private class TestUser : User
        {
            public TestUser(string name, string surname, string email, string phoneNumber, UserRole role)
                : base(name, surname, email, phoneNumber, role) { }
        }

        private class TestBorrowable : Borrowable
        {
            public TestBorrowable(string title, string publisher, bool availability)
                : base(title, publisher, availability) { }
        }

        private class TestEvent : EventBase { }
    }
}
