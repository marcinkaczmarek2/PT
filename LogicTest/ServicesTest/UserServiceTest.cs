using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Logic.Services.Interfaces;
using Logic.Repositories.Interfaces;
using Data.Enums;
using Data.Users;
using Data.Events;
using Data.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.API.Models;

namespace Services.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private UserService _userService;
        private FakeUserRepository _userRepo;
        private FakeEventService _eventService;

        [TestInitialize]
        public void Initialize()
        {
            _userRepo = new FakeUserRepository();
            _eventService = new FakeEventService();
            var userFactory = new UserFactory();
            var eventFactory = new EventFactory();

            _userService = new UserService(_userRepo, _eventService, eventFactory, userFactory);
        }

        [TestMethod]
        public void RemoveUser_ShouldRemove_WhenUserExists()
        {
            var user = new TestUser("John", "Doe", "john@example.com", "123456789", UserRole.Reader);
            _userRepo.AddUser(user);

            bool result = _userService.RemoveUser(user.id);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void RemoveUser_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            bool result = _userService.RemoveUser(Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetUser_ShouldReturnUser_WhenUserExists()
        {
            var user = new TestUser("Jane", "Smith", "jane@example.com", "987654321", UserRole.Reader);
            _userRepo.AddUser(user);

            var result = _userService.GetUser(user.id);

            Assert.IsNotNull(result);
            Assert.AreEqual(user.id, result.id);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Error, no user with such id.")]
        public void GetUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            _userService.GetUser(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnUsers_WhenExist()
        {
            _userRepo.AddUser(new TestUser("User1", "One", "one@example.com", "123", UserRole.Reader));
            _userRepo.AddUser(new TestUser("User2", "Two", "two@example.com", "456", UserRole.Admin));

            var users = _userService.GetAllUsers();

            Assert.AreEqual(2, users.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Error, no users found.")]
        public void GetAllUsers_ShouldThrowException_WhenNoUsersExist()
        {
            _userService.GetAllUsers();
        }

        [TestMethod]
        public void CreateReader_ShouldCreate_WhenValid()
        {
            var result = _userService.CreateReader("Alice", "Wonderland", "alice@example.com", "123456789");

            Assert.IsNotNull(result);
            Assert.AreEqual("Alice", result.name);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Error, User already exists with this email.")]
        public void CreateReader_ShouldThrow_WhenEmailExists()
        {
            var existingUser = new TestUser("Existing", "User", "exists@example.com", "123", UserRole.Reader);
            _userRepo.AddUser(existingUser);

            _userService.CreateReader("New", "User", "exists@example.com", "999");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Error, email must have '@' and '.' signs in it.")]
        public void CreateReader_ShouldThrow_WhenEmailInvalid()
        {
            _userService.CreateReader("New", "User", "invalidemail", "999");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Error, phone number can consist of only numbers.")]
        public void CreateReader_ShouldThrow_WhenPhoneInvalid()
        {
            _userService.CreateReader("New", "User", "user@example.com", "phoneABC");
        }

        [TestMethod]
        public void AddUser_ShouldAddUser_WhenUserIsNew()
        {
            var user = new TestUser("John", "Doe", "john@example.com", "12345", UserRole.Reader);

            bool result = _userService.AddUser(user);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Error, cannot add another user with the same id.")]
        public void AddUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            var user = new TestUser("Jane", "Smith", "jane@example.com", "98765", UserRole.Reader);
            _userRepo.AddUser(user);

            _userService.AddUser(user);
        }

        [TestMethod]
        public void RegisterReader_ShouldReturnFalse_WhenInvalid()
        {
            bool result = _userService.RegisterReader("Bob", "Builder", "bob.example.com", "123456");

            Assert.IsFalse(result);
        }

        private class TestUser : User
        {
            public TestUser(string name, string surname, string email, string phoneNumber, UserRole role)
                : base(name, surname, email, phoneNumber, role) { }
        }

        private class FakeUserRepository : IUserRepository
        {
            private readonly Dictionary<Guid, IUser> _users = new();

            public void AddUser(IUser user)
            {
                if (_users.ContainsKey(user.id))
                    throw new InvalidOperationException("User already exists.");
                _users[user.id] = user;
            }

            public bool RemoveUser(Guid id) => _users.Remove(id);

            public IUser? GetUser(Guid id) => _users.TryGetValue(id, out var user) ? user : null;

            public List<IUser> GetAllUsers() => _users.Values.ToList();
        }


        private class FakeEventService : IEventService
        {
            public bool AddEvent(IEvent eventBase) => true;

            public List<IEvent> GetAllEvents() => new List<IEvent>(); 
        }
    }
}
