using Logic.Services;
using Logic.Repositories;
using Data.Users;
using Data.Implementations;
using Data.Enums;
using Data.API.Models;
using Data.Factories;

namespace Services.Test
{
    [TestClass]
    public class UserServiceTest
    {
        private InMemoryDataContext _context;
        private UserService _userService;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDataContext();
            var userRepository = new UserRepository(_context);
            var eventService = new EventService(new EventRepository(_context));
            var eventFactory = new EventFactory();
            var userFactory = new UserFactory();
            _userService = new UserService(userRepository, eventService, eventFactory, userFactory);
        }

        [TestMethod]
        public void RemoveUser_ShouldRemove_WhenUserExists()
        {
            var reader = new Reader("John", "Doe", "john@example.com", "123456789", UserRole.Reader, 0.0);
            _context.AddUser(reader);

            bool result = _userService.RemoveUser(reader.id);

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
            var reader = new Reader("Jane", "Smith", "jane@example.com", "987654321", UserRole.Reader, 0.0);
            _context.AddUser(reader);

            var result = _userService.GetUser(reader.id);

            Assert.IsNotNull(result);
            Assert.AreEqual(reader.id, result.id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no user with such id.")]
        public void GetUser_ShouldThrowException_WhenUserDoesNotExist()
        {
            _userService.GetUser(Guid.NewGuid());
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnUsers_WhenExist()
        {
            var reader1 = new Reader("User1", "One", "one@example.com", "123", UserRole.Reader, 0.0);
            var reader2 = new Reader("User2", "Two", "two@example.com", "456", UserRole.Admin, 0.0);

            _context.AddUser(reader1);
            _context.AddUser(reader2);

            var users = _userService.GetAllUsers();

            Assert.AreEqual(2, users.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, no users found.")]
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
        [ExpectedException(typeof(Exception), "Error, User already exists with this email.")]
        public void CreateReader_ShouldThrow_WhenEmailExists()
        {
            var existingReader = new Reader("Existing", "User", "exists@example.com", "123", UserRole.Reader, 0.0);
            _context.AddUser(existingReader);

            _userService.CreateReader("New", "User", "exists@example.com", "999");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, email must have '@' and '.' signs in it.")]
        public void CreateReader_ShouldThrow_WhenEmailInvalid()
        {
            _userService.CreateReader("New", "User", "invalidemail", "999");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, phone number can consist of only numbers.")]
        public void CreateReader_ShouldThrow_WhenPhoneInvalid()
        {
            _userService.CreateReader("New", "User", "user@example.com", "phoneABC");
        }

        [TestMethod]
        public void AddUser_ShouldAddUser_WhenUserIsNew()
        {
            var reader = new Reader("John", "Doe", "john@example.com", "12345", UserRole.Reader, 0.0);

            bool result = _userService.AddUser(reader);

            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Error, cannot add another user with the same id.")]
        public void AddUser_ShouldThrowException_WhenUserAlreadyExists()
        {
            var reader = new Reader("Jane", "Smith", "jane@example.com", "98765", UserRole.Reader, 0.0);
            _context.AddUser(reader);

            _userService.AddUser(reader);
        }

        [TestMethod]
        public void RegisterReader_ShouldReturnFalse_WhenInvalid()
        {
            bool result = _userService.RegisterReader("Bob", "Builder", "bob.example.com", "123456");

            Assert.IsFalse(result);
        }
    }
}
