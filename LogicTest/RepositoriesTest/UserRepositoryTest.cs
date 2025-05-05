using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
using Data.Users;
using Data.Enums;
using Data.API.Models;
using Data.API;
using System;
using System.Collections.Generic;

namespace Repositories.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private FakeDataContext _context;
        private UserRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _context = new FakeDataContext();
            _repo = new UserRepository(_context);
        }

        [TestMethod]
        public void AddUser_ShouldAddUser()
        {
            var user = new TestUser("John", "Doe", "john@example.com", "123", UserRole.Reader);

            _repo.AddUser(user);

            Assert.AreEqual(1, _context.Users.Count, "Should have exactly one user added.");
            Assert.AreEqual(user.id, _context.Users[0].id, "User IDs should match.");
        }

        [TestMethod]
        public void RemoveUser_ShouldRemoveUser()
        {
            var user = new TestUser("Jane", "Smith", "jane@example.com", "456", UserRole.Admin);
            _context.Users.Add(user);

            bool removed = _repo.RemoveUser(user.id);

            Assert.IsTrue(removed, "RemoveUser should return true when user is removed.");
            Assert.AreEqual(0, _context.Users.Count, "User list should be empty after removal.");
        }

        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            var user = new TestUser("Mike", "Brown", "mike@example.com", "789", UserRole.Reader);
            _context.Users.Add(user);

            var retrieved = _repo.GetUser(user.id);

            Assert.IsNotNull(retrieved, "Retrieved user should not be null.");
            Assert.AreEqual(user.id, retrieved.id, "User IDs should match.");
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            var user1 = new TestUser("User1", "Last1", "user1@example.com", "111", UserRole.Reader);
            var user2 = new TestUser("User2", "Last2", "user2@example.com", "222", UserRole.Admin);

            _context.Users.Add(user1);
            _context.Users.Add(user2);

            var users = _repo.GetAllUsers();

            Assert.AreEqual(2, users.Count, "Should return exactly two users.");
        }
        private class FakeDataContext : IData
        {
            public List<IUser> Users { get; } = new();

            public void AddUser(IUser user) => Users.Add(user);

            public bool DeleteUser(Guid id) => Users.RemoveAll(u => u.id == id) > 0;

            public IUser? GetUser(Guid id) => Users.Find(u => u.id == id);

            public List<IUser> GetUsers() => Users;
            public void AddItem(IBorrowable b) => throw new NotImplementedException();
            public bool DeleteItem(Guid id) => throw new NotImplementedException();
            public IBorrowable? GetItem(Guid id) => throw new NotImplementedException();
            public List<IBorrowable> GetItems() => throw new NotImplementedException();
            public List<IEvent> GetEvents() => throw new NotImplementedException();
            public void AddEvent(IEvent eventBase) => throw new NotImplementedException();
        }

        private class TestUser : User
        {
            public TestUser(string name, string surname, string email, string phoneNumber, UserRole role)
                : base(name, surname, email, phoneNumber, role) { }
        }
    }
}
