using Data.Users;

namespace Users.Tests
{
    [TestClass]
    public class ReaderTest
    {
        [TestMethod]
        public void ReaderConstructorTest()
        {
            var name = "Bob";
            var surname = "Brown";
            var email = "bob.brown@example.com";
            var phoneNumber = "111222333";
            var role = UserRole.Reader;
            var debt = 0.0;

            var reader = new Reader(name, surname, email, phoneNumber, role, debt);

            Assert.AreEqual(name, reader.name);
            Assert.AreEqual(surname, reader.surname);
            Assert.AreEqual(email, reader.email);
            Assert.AreEqual(phoneNumber, reader.phoneNumber);
            Assert.AreEqual(role, reader.role);
            Assert.AreNotEqual(Guid.Empty, reader.id);
            Assert.IsNotNull(reader.borrowedBooks);
            Assert.AreEqual(0, reader.borrowedBooks.Count);
        }

        [TestMethod]
        public void Reader_BorrowBook_AddsBookId()
        {
            var reader = new Reader("Bob", "Brown", "bob@example.com", "111222333", UserRole.Reader, 0.0);
            var bookId = Guid.NewGuid();

            reader.BorrowBook(bookId);

            Assert.IsTrue(reader.borrowedBooks.Contains(bookId));
        }

        [TestMethod]
        public void Reader_ReturnBook_RemovesBookId()
        {
            var reader = new Reader("Bob", "Brown", "bob@example.com", "111222333", UserRole.Reader, 0.0);
            var bookId = Guid.NewGuid();
            reader.BorrowBook(bookId);

            reader.ReturnBook(bookId);

            Assert.IsFalse(reader.borrowedBooks.Contains(bookId));
        }
    }
}
