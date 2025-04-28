using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;
using Data.Users;
using Data.Events;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Services.Test
{
    [TestClass]
    public class UserServiceTest
    {
        [TestMethod]
        public void RemoveUser_ShouldRemove_WhenUserExists()
        {
            var user = new TestUser("John", "Doe", "john@example.com", "123456789", UserRole.Reader);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetUser(user.id)).Returns(user);
            userRepoMock.Setup(r => r.RemoveUser(user.id)).Returns(true);

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            var result = service.RemoveUser(user.id);

            Assert.IsTrue(result);
            eventServiceMock.Verify(e => e.AddEvent(It.IsAny<UserRemovedEvent>()), Times.Once);
        }

        [TestMethod]
        public void RemoveUser_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetUser(It.IsAny<Guid>())).Returns((User?)null);

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            var result = service.RemoveUser(Guid.NewGuid());

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GetUser_ShouldReturnUser_WhenUserExists()
        {
            var user = new TestUser("Jane", "Smith", "jane@example.com", "987654321", UserRole.Reader);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetUser(user.id)).Returns(user);

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            var result = service.GetUser(user.id);

            Assert.IsNotNull(result);
            Assert.AreEqual(user.id, result.id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no user with such id.")]
        public void GetUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetUser(It.IsAny<Guid>())).Returns((User?)null);

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            service.GetUser(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnUsers_WhenExist()
        {
            var users = new List<User>
            {
                new TestUser("User1", "One", "one@example.com", "123", UserRole.Reader),
                new TestUser("User2", "Two", "two@example.com", "456", UserRole.Admin)
            };

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetAllUsers()).Returns(users);

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            var result = service.GetAllUsers();

            Assert.AreEqual(2, result.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no users found.")]
        public void GetAllUsers_ShouldThrowException_WhenNoUsersExist()
        {
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetAllUsers()).Returns(new List<User>());

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            service.GetAllUsers();
        }

        [TestMethod]
        public void CreateReader_ShouldCreate_WhenValid()
        {
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetAllUsers()).Returns(new List<User>());

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            var result = service.CreateReader("Alice", "Wonderland", "alice@example.com", "123456789");

            Assert.IsNotNull(result);
            Assert.AreEqual("Alice", result.name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, User already exists with this email.")]
        public void CreateReader_ShouldThrow_WhenEmailExists()
        {
            var existingUser = new TestUser("Existing", "User", "exists@example.com", "123", UserRole.Reader);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetAllUsers()).Returns(new List<User> { existingUser });

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            service.CreateReader("New", "User", "exists@example.com", "999");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, email must have '@' and '.' signs in it.")]
        public void CreateReader_ShouldThrow_WhenEmailInvalid()
        {
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetAllUsers()).Returns(new List<User>());

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            service.CreateReader("New", "User", "invalidemail", "999");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, phone number can consist of only numbers.")]
        public void CreateReader_ShouldThrow_WhenPhoneInvalid()
        {
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetAllUsers()).Returns(new List<User>());

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            service.CreateReader("New", "User", "user@example.com", "phoneABC");
        }

        [TestMethod]
        public void AddUser_ShouldAddUser_WhenUserIsNew()
        {
            // Arrange
            var user = new Reader("John", "Doe", "john@example.com", "12345", UserRole.Reader, 0.0);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetUser(user.id)).Returns((User?)null);

            var eventServiceMock = new Mock<IEventService>();
            eventServiceMock.Setup(e => e.AddEvent(It.IsAny<UserAddedEvent>())).Verifiable();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            // Act
            var result = service.AddUser(user);

            // Assert
            Assert.IsTrue(result);
            eventServiceMock.Verify(e => e.AddEvent(It.IsAny<UserAddedEvent>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, cannot add another user with the same id.")]
        public void AddUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            // Arrange
            var user = new Reader("Jane", "Smith", "jane@example.com", "98765", UserRole.Reader, 0.0);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetUser(user.id)).Returns(user); // Symulujemy że user już istnieje

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            // Act + Assert (spodziewamy się wyjątku)
            service.AddUser(user);
        }

        [TestMethod]
        public void RegisterReader_ShouldReturnFalse_WhenInvalid()
        {
            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetAllUsers()).Returns(new List<User>());

            var eventServiceMock = new Mock<IEventService>();

            var service = new UserService(userRepoMock.Object, eventServiceMock.Object);

            var result = service.RegisterReader("Bob", "Builder", "bob.example.com", "123456");

            Assert.IsFalse(result);
        }

        private class TestUser : User
        {
            public TestUser(string name, string surname, string email, string phoneNumber, UserRole role)
                : base(name, surname, email, phoneNumber, role) { }
        }
    }
}
