using Microsoft.VisualStudio.TestTools.UnitTesting;
using Data.Users;

namespace Users.Tests
{
    [TestClass]
    public class EmployeeTest
    {
        [TestMethod]
        public void EmployeeConstructorTest()
        {
            var name = "Alice";
            var surname = "Smith";
            var email = "alice.smith@example.com";
            var phoneNumber = "987654321";
            var role = UserRole.Admin;
            var salary = 5000.00;

            var employee = new Employee(name, surname, email, phoneNumber, role, salary);

            Assert.AreEqual(name, employee.name);
            Assert.AreEqual(surname, employee.surname);
            Assert.AreEqual(email, employee.email);
            Assert.AreEqual(phoneNumber, employee.phoneNumber);
            Assert.AreEqual(role, employee.role);
            Assert.AreEqual(salary, employee.salary);
            Assert.AreNotEqual(Guid.Empty, employee.id);
        }
    }
}
