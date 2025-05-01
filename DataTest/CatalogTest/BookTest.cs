using Data.Catalog;

using Data.Enums;
namespace Catalog.Tests
{
    [TestClass]
    public class BookTest
    {
        [TestMethod]
        public void BookConstructorTest()
        {
            string title = "The Hobbit";
            string publisher = "George Allen & Unwin";
            bool availability = true;
            string author = "J.R.R. Tolkien";
            int numberOfPages = 310;
            BookGenre genre = BookGenre.Fantasy;

            var book = new Book(title, publisher, availability, author, numberOfPages, genre);

            Assert.AreEqual(title, book.title);
            Assert.AreEqual(publisher, book.publisher);
            Assert.AreEqual(availability, book.availability);
            Assert.AreEqual(author, book.author);
            Assert.AreEqual(numberOfPages, book.numberOfPages);
            Assert.AreEqual(genre, book.genre);
        }
    }
}
