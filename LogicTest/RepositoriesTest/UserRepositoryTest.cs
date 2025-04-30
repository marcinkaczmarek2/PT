using Logic.Repositories;
using Data.Users;
using Data.Implementations;

namespace Repositories.Test
{
    [TestClass]
    public class UserRepositoryTest
    {
        private InMemoryDataContext _context;
        private UserRepository _repo;

        [TestInitialize]
        public void Initialize()
        {
            _context = new InMemoryDataContext();
            _repo = new UserRepository(_context);
        }

        [TestMethod]
        public void AddUser_ShouldAddUser()
        {
            var reader = new Reader("John", "Doe", "john@example.com", "123", UserRole.Reader, 0.0);

            _repo.AddUser(reader);

            List<User> users = _context.GetUsers();
            Assert.AreEqual(1, users.Count, "Should have exactly one user added.");
            Assert.AreEqual(reader.id, users[0].id, "User IDs should match.");
        }

        [TestMethod]
        public void RemoveUser_ShouldRemoveUser()
        {
            var employee = new Employee("Jane", "Smith", "jane@example.com", "456", UserRole.Admin, 5000.0);
            _context.AddUser(employee);

            bool removed = _repo.RemoveUser(employee.id);

            Assert.IsTrue(removed, "RemoveUser should return true when user is removed.");
            Assert.AreEqual(0, _context.GetUsers().Count, "Context should be empty after removal.");
        }

        [TestMethod]
        public void GetUser_ShouldReturnCorrectUser()
        {
            var reader = new Reader("Mike", "Brown", "mike@example.com", "789", UserRole.Reader, 0.0);
            _context.AddUser(reader);

            var retrieved = _repo.GetUser(reader.id);

            Assert.IsNotNull(retrieved, "Retrieved user should not be null.");
            Assert.AreEqual(reader.id, retrieved.id, "User IDs should match.");
        }

        [TestMethod]
        public void GetAllUsers_ShouldReturnAllUsers()
        {
            var reader = new Reader("User1", "Last1", "user1@example.com", "111", UserRole.Reader, 0.0);
            var employee = new Employee("User2", "Last2", "user2@example.com", "222", UserRole.Admin, 6000.0);

            _context.AddUser(reader);
            _context.AddUser(employee);

            var users = _repo.GetAllUsers();

            Assert.AreEqual(2, users.Count, "Should return exactly two users.");
        }
    }
}
