using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var user = new FakeUser("John", "Doe", "john@example.com", "123", FakeRole.Reader);

            _repo.AddUser(user);

            Assert.AreEqual(1, _context.Users.Count);
            Assert.AreEqual(user.Id, _context.Users[0].Id);
        }

        [TestMethod]
        public void RemoveUser_ShouldRemoveUser()
        {
            var user = new FakeUser("Jane", "Smith", "jane@example.com", "456", FakeRole.Admin);
            _context.Users.Add(user);

            bool removed = _repo.RemoveUser(user.Id);

            Assert.IsTrue(removed);
            Assert.AreEqual(0, _context.Users.Count);
        }

        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            var user = new FakeUser("Mike", "Brown", "mike@example.com", "789", FakeRole.Reader);
            _context.Users.Add(user);

            var retrieved = _repo.GetUser(user.Id);

            Assert.IsNotNull(retrieved);
            Assert.AreEqual(user.Id, retrieved.Id);
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            var user1 = new FakeUser("User1", "Last1", "user1@example.com", "111", FakeRole.Reader);
            var user2 = new FakeUser("User2", "Last2", "user2@example.com", "222", FakeRole.Admin);

            _context.Users.Add(user1);
            _context.Users.Add(user2);

            var users = _repo.GetAllUsers();

            Assert.AreEqual(2, users.Count);
        }

        public interface IUserFake
        {
            Guid Id { get; }
            string Name { get; }
            string Surname { get; }
            string Email { get; }
            string PhoneNumber { get; }
            FakeRole Role { get; }
        }

        public enum FakeRole
        {
            Reader,
            Librarian,
            Admin
        }

        public class FakeUser : IUserFake
        {
            public Guid Id { get; } = Guid.NewGuid();
            public string Name { get; }
            public string Surname { get; }
            public string Email { get; }
            public string PhoneNumber { get; }
            public FakeRole Role { get; }

            public FakeUser(string name, string surname, string email, string phoneNumber, FakeRole role)
            {
                Name = name;
                Surname = surname;
                Email = email;
                PhoneNumber = phoneNumber;
                Role = role;
            }
        }

        public interface ILogicUserData
        {
            void AddUser(IUserFake user);
            bool DeleteUser(Guid id);
            IUserFake? GetUser(Guid id);
            List<IUserFake> GetUsers();
        }

        public class FakeDataContext : ILogicUserData
        {
            public List<IUserFake> Users { get; } = new();

            public void AddUser(IUserFake user) => Users.Add(user);
            public bool DeleteUser(Guid id) => Users.RemoveAll(u => u.Id == id) > 0;
            public IUserFake? GetUser(Guid id) => Users.Find(u => u.Id == id);
            public List<IUserFake> GetUsers() => Users;
        }

        public class UserRepository
        {
            private readonly ILogicUserData context;

            public UserRepository(ILogicUserData context)
            {
                this.context = context;
            }

            public void AddUser(IUserFake user) => context.AddUser(user);
            public bool RemoveUser(Guid id) => context.DeleteUser(id);
            public IUserFake? GetUser(Guid id) => context.GetUser(id);
            public List<IUserFake> GetAllUsers() => context.GetUsers();
        }
    }
}
