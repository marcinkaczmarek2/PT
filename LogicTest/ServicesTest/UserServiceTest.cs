using Logic.Services.Interfaces;
using Logic.Repositories.Interfaces;
using Data.API.Models;
using Logic.Services.Implementations;
using Data.Implementations.Enums;


namespace LogicTest.ServicesTest
{
    [TestClass]
    public class UserServiceTest
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

        private class FakeEvent : IEvent
        {
            public Guid eventId { get; } = Guid.NewGuid();
            public DateTime timestamp { get; set; } = DateTime.Now;
        }

        private class FakeUserFactory : IUserFactory
        {
            public IUser CreateReader(string name, string surname, string email, string phoneNumber)
            {
                return new FakeUser
                {
                    id = Guid.NewGuid(),
                    name = name,
                    surname = surname,
                    email = email,
                    phoneNumber = phoneNumber,
                    role = UserRole.Reader
                };
            }
        }

        private class FakeUserRepo : IUserRepository
        {
            private readonly Dictionary<Guid, IUser> users = new();

            public void AddUser(IUser user) => users[user.id] = user;
            public IUser? GetUser(Guid id) => users.TryGetValue(id, out var u) ? u : null;
            public List<IUser> GetAllUsers() => new(users.Values);
            public bool RemoveUser(Guid id) => users.Remove(id);
        }

        private class FakeEventService : IEventService
        {
            public List<IEvent> Events { get; } = new();
            public bool AddEvent(IEvent eventBase)
            {
                Events.Add(eventBase);
                return true;
            }
            public List<IEvent> GetAllEvents() => new(Events);
        }

        private class FakeEventFactory : IEventFactory
        {
            public IEvent CreateUserAddedEvent(Guid userId, string userEmail) => new FakeEvent();
            public IEvent CreateUserRemovedEvent(Guid userId, string userEmail) => new FakeEvent();
            public IEvent CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
            public IEvent CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
            public IEvent CreateItemAddedEvent(Guid itemId, string itemTitle) => new FakeEvent();
            public IEvent CreateItemRemovedEvent(Guid itemId, string itemTitle) => new FakeEvent();
        }

        private UserService CreateService(out FakeUserRepo repo, out FakeEventService events)
        {
            repo = new FakeUserRepo();
            events = new FakeEventService();
            return new UserService(repo, events, new FakeEventFactory(), new FakeUserFactory());
        }

        [TestMethod]
        public void AddUser_Success()
        {
            var service = CreateService(out var repo, out var events);
            var user = new FakeUser { id = Guid.NewGuid(), email = "a@a.com" };

            var result = service.AddUser(user);

            Assert.IsTrue(result);
            Assert.AreEqual(1, events.GetAllEvents().Count);
            Assert.AreEqual(user, repo.GetUser(user.id));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddUser_DuplicateId_Throws()
        {
            var service = CreateService(out var repo, out _);
            var id = Guid.NewGuid();
            repo.AddUser(new FakeUser { id = id, email = "x@x.com" });

            service.AddUser(new FakeUser { id = id, email = "y@y.com" });
        }

        [TestMethod]
        public void RemoveUser_Success()
        {
            var service = CreateService(out var repo, out var events);
            var id = Guid.NewGuid();
            var user = new FakeUser { id = id, email = "user@test.com" };
            repo.AddUser(user);

            var result = service.RemoveUser(id);

            Assert.IsTrue(result);
            Assert.IsNull(repo.GetUser(id));
            Assert.AreEqual(1, events.GetAllEvents().Count);
        }

        [TestMethod]
        public void RemoveUser_NotFound_ReturnsFalse()
        {
            var service = CreateService(out _, out var events);

            var result = service.RemoveUser(Guid.NewGuid());

            Assert.IsFalse(result);
            Assert.AreEqual(0, events.GetAllEvents().Count);
        }

        [TestMethod]
        public void GetUser_Existing_ReturnsUser()
        {
            var service = CreateService(out var repo, out _);
            var user = new FakeUser { id = Guid.NewGuid() };
            repo.AddUser(user);

            var result = service.GetUser(user.id);

            Assert.AreEqual(user, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetUser_NotFound_Throws()
        {
            var service = CreateService(out _, out _);
            service.GetUser(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllUsers_ReturnsUsers()
        {
            var service = CreateService(out var repo, out _);
            repo.AddUser(new FakeUser { id = Guid.NewGuid() });

            var result = service.GetAllUsers();

            Assert.AreEqual(1, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void GetAllUsers_Empty_Throws()
        {
            var service = CreateService(out _, out _);
            service.GetAllUsers();
        }

        [TestMethod]
        public void CreateReader_ValidInput_Success()
        {
            var service = CreateService(out var repo, out _);

            var reader = service.CreateReader("Anna", "Nowak", "anna@mail.com", "+48123456789");

            Assert.AreEqual("Anna", reader.name);
            Assert.AreEqual("Nowak", reader.surname);
            Assert.AreEqual("anna@mail.com", reader.email);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateReader_DuplicateEmail_Throws()
        {
            var service = CreateService(out var repo, out _);
            repo.AddUser(new FakeUser { id = Guid.NewGuid(), email = "dup@ma.il" });

            service.CreateReader("X", "Y", "dup@ma.il", "123");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateReader_InvalidEmail_Throws()
        {
            var service = CreateService(out _, out _);
            service.CreateReader("X", "Y", "noat", "123");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CreateReader_InvalidPhone_Throws()
        {
            var service = CreateService(out _, out _);
            service.CreateReader("X", "Y", "test@mail.com", "12A3");
        }

        [TestMethod]
        public void RegisterReader_Valid_ReturnsTrue()
        {
            var service = CreateService(out _, out _);
            var result = service.RegisterReader("X", "Y", "x@y.com", "123456");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RegisterReader_InvalidEmail_ReturnsFalse()
        {
            var service = CreateService(out _, out _);
            var result = service.RegisterReader("X", "Y", "bademail", "123");
            Assert.IsFalse(result);
        }
    }
}

