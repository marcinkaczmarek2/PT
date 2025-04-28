using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
using Data.Users;
using Data;
using System.Collections.Generic;

namespace Repositories.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        [TestMethod]
        public void AddUser_ShouldAddUser()
        {
            var context = new InMemoryDataContext();
            var repo = new UserRepository(context);
            var user = new TestUser("John", "Doe", "john@example.com", "123", UserRole.Reader);

            repo.AddUser(user);

            Assert.AreEqual(1, context.GetUsers().Count);
        }

        [TestMethod]
        public void RemoveUser_ShouldRemoveUser()
        {
            var context = new InMemoryDataContext();
            var repo = new UserRepository(context);
            var user = new TestUser("Jane", "Smith", "jane@example.com", "456", UserRole.Admin);
            context.AddUser(user);

            var removed = repo.RemoveUser(user.id);

            Assert.IsTrue(removed);
            Assert.AreEqual(0, context.GetUsers().Count);
        }

        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            var context = new InMemoryDataContext();
            var repo = new UserRepository(context);
            var user = new TestUser("Mike", "Brown", "mike@example.com", "789", UserRole.Guest);
            context.AddUser(user);

            var retrieved = repo.GetUser(user.id);

            Assert.IsNotNull(retrieved);
            Assert.AreEqual(user.id, retrieved.id);
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            var context = new InMemoryDataContext();
            var repo = new UserRepository(context);
            context.AddUser(new TestUser("User1", "Last1", "user1@example.com", "111", UserRole.Reader));
            context.AddUser(new TestUser("User2", "Last2", "user2@example.com", "222", UserRole.Admin));

            var users = repo.GetAllUsers();

            Assert.AreEqual(2, users.Count);
        }

        private class TestUser : User
        {
            public TestUser(string name, string surname, string email, string phoneNumber, UserRole role)
                : base(name, surname, email, phoneNumber, role) { }
        }
    }
}
