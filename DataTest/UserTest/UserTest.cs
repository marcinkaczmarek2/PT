using Data.Users;
using Data.Enums;
namespace Users.Tests
{
    [TestClass]
    public class UserTest
    {
        private class TestUser : User
        {
            public TestUser(string name, string surname, string email, string phoneNumber, UserRole role)
                : base(name, surname, email, phoneNumber, role) { }
        }

        [TestMethod]
        public void UserConstructorTest()
        {
            var name = "John";
            var surname = "Doe";
            var email = "john.doe@example.com";
            var phoneNumber = "123456789";
            var role = UserRole.Reader;

            var user = new TestUser(name, surname, email, phoneNumber, role);

            Assert.AreEqual(name, user.name);
            Assert.AreEqual(surname, user.surname);
            Assert.AreEqual(email, user.email);
            Assert.AreEqual(phoneNumber, user.phoneNumber);
            Assert.AreEqual(role, user.role);
            Assert.AreNotEqual(Guid.Empty, user.id);
        }
    }
}
