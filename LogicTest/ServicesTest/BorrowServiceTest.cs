    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using Logic.Models;
    using Logic.Repositories.Interfaces;
    using Logic.Services;
    using Logic.Services.Interfaces;

namespace Services.Test
{
    [TestClass]
    public class BorrowServiceTest
    {
        private BorrowService _service;
        private FakeUserRepository _userRepo;
        private FakeLibraryRepository _libraryRepo;
        private FakeEventService _eventService;
        private FakeEventFactory _eventFactory;

        private Guid _userId;
        private Guid _itemId;

        [TestInitialize]
        public void Init()
        {
            _userRepo = new FakeUserRepository();
            _libraryRepo = new FakeLibraryRepository();
            _eventService = new FakeEventService();
            _eventFactory = new FakeEventFactory();

            _service = new BorrowService(_userRepo, _libraryRepo, _eventService, _eventFactory);

            var user = new FakeUser();
            var item = new FakeItem { availability = true };

            _userId = user.Id;
            _itemId = item.Id;

            _userRepo.AddUser(user);
            _libraryRepo.AddContent(item);
        }

        [TestMethod]
        public void BorrowItem_Success()
        {
            var result = _service.BorrowItem(_userId, _itemId);
            Assert.IsTrue(result);
            Assert.IsFalse(_libraryRepo.GetContent(_itemId)!.availability);
            Assert.AreEqual(1, _eventService.Events.Count);
        }

        [TestMethod]
        public void ReturnItem_Success()
        {
            _libraryRepo.GetContent(_itemId)!.availability = false;

            var result = _service.ReturnItem(_userId, _itemId);

            Assert.IsTrue(result);
            Assert.IsTrue(_libraryRepo.GetContent(_itemId)!.availability);
            Assert.AreEqual(1, _eventService.Events.Count);
        }

        // === Fakes ===

        private class FakeUser : IUser
        {
            public Guid Id { get; } = Guid.NewGuid();
            public string Name { get; set; } = "Test";
            public string Surname { get; set; } = "User";
            public string Email { get; set; } = "test@example.com";
            public string PhoneNumber { get; set; } = "+123456789";
            public UserRole Role { get; set; } = UserRole.Reader;
        }

        private class FakeItem : IBorrowableL
        {
            public Guid Id { get; } = Guid.NewGuid();
            public string Title { get; set; } = "Test Book";
            public string Publisher { get; set; } = "Publisher";
            public bool availability { get; set; }
        }

        private class FakeEvent : IEventL
        {
            public Guid EventId { get; } = Guid.NewGuid();
            public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        }

        private class FakeEventFactory : IEventFactory
        {
            public IEventL CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
            public IEventL CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle) => new FakeEvent();
            public IEventL CreateItemAddedEvent(Guid itemId, string itemTitle) => new FakeEvent();
            public IEventL CreateItemRemovedEvent(Guid itemId, string itemTitle) => new FakeEvent();
            public IEventL CreateUserAddedEvent(Guid userId, string userEmail) => new FakeEvent();
            public IEventL CreateUserRemovedEvent(Guid userId, string userEmail) => new FakeEvent();
        }

        private class FakeUserRepository : IUserRepository
        {
            private readonly Dictionary<Guid, IUser> users = new();
            public void AddUser(IUser user) => users[user.Id] = user;
            public bool RemoveUser(Guid id) => users.Remove(id);
            public IUser? GetUser(Guid id) => users.TryGetValue(id, out var u) ? u : null;
            public List<IUser> GetAllUsers() => new(users.Values);
        }

        private class FakeLibraryRepository : ILibraryRepository
        {
            private readonly Dictionary<Guid, IBorrowableL> items = new();
            public void AddContent(IBorrowableL item) => items[item.Id] = item;
            public bool RemoveContent(Guid id) => items.Remove(id);
            public IBorrowableL? GetContent(Guid id) => items.TryGetValue(id, out var i) ? i : null;
            public List<IBorrowableL> GetAllContent() => new(items.Values);
        }

        private class FakeEventService : IEventService
        {
            public List<IEventL> Events { get; } = new();
            public bool AddEvent(IEventL e) { Events.Add(e); return true; }
            public List<IEventL> GetAllEvents() => Events;
        }
    }
}