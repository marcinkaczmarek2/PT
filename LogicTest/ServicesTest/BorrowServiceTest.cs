using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Data.Users;
using Data.Catalog;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;
using System;
using Moq;

namespace Services.Test
{
    [TestClass]
    public class BorrowServiceTest
    {
        [TestMethod]
        public void BorrowItem_ShouldSetItemAsUnavailable()
        {
            var user = new TestUser("John", "Doe", "john@example.com", "123", UserRole.Reader);
            var item = new TestBorrowable("Book", "Publisher", true);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetUser(user.id)).Returns(user);

            var libraryRepoMock = new Mock<ILibraryRepository>();
            libraryRepoMock.Setup(r => r.GetContent(item.id)).Returns(item);

            var eventServiceMock = new Mock<IEventService>();

            var borrowService = new BorrowService(userRepoMock.Object, libraryRepoMock.Object, eventServiceMock.Object);

            var result = borrowService.BorrowItem(user.id, item.id);

            Assert.IsTrue(result);
            Assert.IsFalse(item.availbility);
        }

        [TestMethod]
        public void ReturnItem_ShouldSetItemAsAvailable()
        {
            var user = new TestUser("Jane", "Doe", "jane@example.com", "456", UserRole.Reader);
            var item = new TestBorrowable("Game", "Publisher", false);

            var userRepoMock = new Mock<IUserRepository>();
            userRepoMock.Setup(r => r.GetUser(user.id)).Returns(user);

            var libraryRepoMock = new Mock<ILibraryRepository>();
            libraryRepoMock.Setup(r => r.GetContent(item.id)).Returns(item);

            var eventServiceMock = new Mock<IEventService>();

            var borrowService = new BorrowService(userRepoMock.Object, libraryRepoMock.Object, eventServiceMock.Object);

            var result = borrowService.ReturnItem(user.id, item.id);

            Assert.IsTrue(result);
            Assert.IsTrue(item.availbility);
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
    }
}
