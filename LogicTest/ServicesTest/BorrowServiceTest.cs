using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Services;
using Logic.Repositories.Interfaces;
using Data.API.Models;
using Data.Enums;
using System;
using System.Collections.Generic;
using Logic.Services.Interfaces;
using Data.Events;

namespace Services.Test
{
    [TestClass]
    public class BorrowServiceTest
    {
        private FakeUserRepository _userRepository;
        private FakeLibraryRepository _libraryRepository;
        private FakeEventService _eventService;
        private BorrowService _borrowService;

        private Guid _userId;
        private Guid _itemId;

        [TestInitialize]
        public void Initialize()
        {
            _userRepository = new FakeUserRepository();
            _libraryRepository = new FakeLibraryRepository();
            _eventService = new FakeEventService();

            _borrowService = new BorrowService(_userRepository, _libraryRepository, _eventService, null);

            var user = new TestUser("John", "Doe", "john@example.com", "+123", UserRole.Reader);
            var item = new TestBorrowable("The Great Adventure", "Publisher", true);

            _userRepository.AddUser(user);
            _libraryRepository.AddContent(item);

            _userId = user.id;
            _itemId = item.id;
        }
        private class FakeUserRepository : IUserRepository
        {
            private readonly Dictionary<Guid, IUser> _users = new();

            public void AddUser(IUser user) => _users[user.id] = user;
            public IUser? GetUser(Guid id) => _users.TryGetValue(id, out var user) ? user : null;
            public List<IUser> GetAllUsers() => new(_users.Values);
            public bool RemoveUser(Guid id) => _users.Remove(id);
        }

        private class FakeLibraryRepository : ILibraryRepository
        {
            private readonly Dictionary<Guid, IBorrowable> _items = new();

            public void AddContent(IBorrowable item) => _items[item.id] = item;
            public IBorrowable? GetContent(Guid id) => _items.TryGetValue(id, out var item) ? item : null;
            public List<IBorrowable> GetAllContent() => new(_items.Values);
            public bool RemoveContent(Guid id) => _items.Remove(id);
        }

        private class FakeEventService : IEventService
        {
            public bool AddEvent(IEvent e) => true;
            public List<IEvent> GetAllEvents() => new();
        }

        private class TestUser : IUser
        {
            public Guid id { get; } = Guid.NewGuid();
            public string name { get; set; }
            public string surname { get; set; }
            public string email { get; set; }
            public string phoneNumber { get; set; }
            public UserRole role { get; set; }

            public TestUser(string name, string surname, string email, string phoneNumber, UserRole role)
            {
                this.name = name;
                this.surname = surname;
                this.email = email;
                this.phoneNumber = phoneNumber;
                this.role = role;
            }
        }

        private class TestBorrowable : IBorrowable
        {
            public Guid id { get; } = Guid.NewGuid();
            public string title { get; set; }
            public string publisher { get; set; }
            public bool availability { get; set; }

            public TestBorrowable(string title, string publisher, bool availability)
            {
                this.title = title;
                this.publisher = publisher;
                this.availability = availability;
            }
        }
    }
}
