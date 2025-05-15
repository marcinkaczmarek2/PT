using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;
using Data.API.Models;
using Logic.Services.Implementations;
using Data.Implementations.Enums;

namespace LogicTest.ServicesTest
{
    [TestClass]
    public class BorrowServiceTest
    {
        private class FakeUser : IUser
        {
            public Guid id { get; set; }
            public string name { get; set; } = "";
            public string surname { get; set; } = "";
            public string email { get; set; } = "";
            public string phoneNumber { get; set; } = "";
            public UserRole role { get; set; }
        }

        private class FakeItem : IBorrowable
        {
            public Guid id { get; set; }
            public string title { get; set; } = "";
            public string publisher { get; set; } = "";
            public bool availability { get; set; } = true;
        }

        private class FakeEvent : IEvent
        {
            public Guid eventId { get; } = Guid.NewGuid();
            public DateTime timestamp { get; set; } = DateTime.Now;
        }

        private class FakeUserRepo : IUserRepository
        {
            private readonly Dictionary<Guid, IUser> users = new();

            public void AddUser(IUser user) => users[user.id] = user;

            public IUser? GetUser(Guid id) => users.TryGetValue(id, out var u) ? u : null;

            public List<IUser> GetAllUsers() => new(users.Values);

            public bool RemoveUser(Guid id) => users.Remove(id);
        }


        private class FakeLibraryRepo : ILibraryRepository
        {
            private readonly Dictionary<Guid, IBorrowable> items = new();

            public void AddContent(IBorrowable content) => items[content.id] = content;

            public bool RemoveContent(Guid id) => items.Remove(id);

            public IBorrowable? GetContent(Guid id) => items.TryGetValue(id, out var item) ? item : null;

            public List<IBorrowable> GetAllContent() => new(items.Values);
        }


        private class FakeEventService : IEventService
        {
            private readonly List<IEvent> events = new();

            public bool AddEvent(IEvent eventBase)
            {
                events.Add(eventBase);
                return true;
            }

            public List<IEvent> GetAllEvents() => new(events);

            public List<IEvent> Events => events;
        }

        private class FakeEventFactory : IEventFactory
        {
            public IEvent CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
            public IEvent CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
            public IEvent CreateItemAddedEvent(Guid itemId, string itemTitle) => new FakeEvent();
            public IEvent CreateItemRemovedEvent(Guid itemId, string itemTitle) => new FakeEvent();
            public IEvent CreateUserAddedEvent(Guid userId, string userEmail) => new FakeEvent();
            public IEvent CreateUserRemovedEvent(Guid userId, string userEmail) => new FakeEvent();
        }

        private BorrowService CreateService(out FakeUserRepo userRepo, out FakeLibraryRepo libRepo, out FakeEventService eventSvc)
        {
            userRepo = new FakeUserRepo();
            libRepo = new FakeLibraryRepo();
            eventSvc = new FakeEventService();

            return new BorrowService(userRepo, libRepo, eventSvc, new FakeEventFactory());
        }

        [TestMethod]
        public void BorrowItem_Success()
        {
            var service = CreateService(out var users, out var items, out var events);
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            users.AddUser(new FakeUser { id = userId });
            items.AddContent(new FakeItem { id = itemId, availability = true, title = "Title" });

            var result = service.BorrowItem(userId, itemId);

            Assert.IsTrue(result);
            Assert.IsFalse(items.GetContent(itemId)!.availability);
            Assert.AreEqual(1, events.Events.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BorrowItem_UserNotFound()
        {
            var service = CreateService(out _, out var items, out _);
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            items.AddContent(new FakeItem { id = itemId, availability = true, title = "Title" });

            service.BorrowItem(userId, itemId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BorrowItem_ItemNotFound()
        {
            var service = CreateService(out var users, out _, out _);
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            users.AddUser(new FakeUser { id = userId });

            service.BorrowItem(userId, itemId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void BorrowItem_ItemUnavailable()
        {
            var service = CreateService(out var users, out var items, out _);
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            users.AddUser(new FakeUser { id = userId });
            items.AddContent(new FakeItem { id = itemId, availability = false, title = "Title" });

            service.BorrowItem(userId, itemId);
        }

        [TestMethod]
        public void ReturnItem_Success()
        {
            var service = CreateService(out var users, out var items, out var events);
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            users.AddUser(new FakeUser { id = userId });
            items.AddContent(new FakeItem { id = itemId, availability = false, title = "Title" });

            var result = service.ReturnItem(userId, itemId);

            Assert.IsTrue(result);
            Assert.IsTrue(items.GetContent(itemId)!.availability);
            Assert.AreEqual(1, events.Events.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReturnItem_UserNotFound()
        {
            var service = CreateService(out _, out var items, out _);
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            items.AddContent(new FakeItem { id = itemId, availability = false, title = "Title" });

            service.ReturnItem(userId, itemId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReturnItem_ItemNotFound()
        {
            var service = CreateService(out var users, out _, out _);
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            users.AddUser(new FakeUser { id = userId });

            service.ReturnItem(userId, itemId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ReturnItem_ItemNotBorrowed()
        {
            var service = CreateService(out var users, out var items, out _);
            var userId = Guid.NewGuid();
            var itemId = Guid.NewGuid();
            users.AddUser(new FakeUser { id = userId });
            items.AddContent(new FakeItem { id = itemId, availability = true, title = "Title" });

            service.ReturnItem(userId, itemId);
        }
    }

}
