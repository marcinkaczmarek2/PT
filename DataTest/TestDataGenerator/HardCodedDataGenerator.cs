using Data.API;
using Data.Implementations.Catalog;
using Data.Implementations.Users;
using Data.Implementations;
using Data.Implementations.Enums;

namespace DataTest.TestDataGenerator
{
    internal class HardcodedDataGenerator : IDataGenerator
    {
        private readonly IData _data;

        public HardcodedDataGenerator()
        {
            _data = new InMemoryDataContext();
            AddStaticData();
        }

        public IData GetData() => _data;

        private void AddStaticData()
        {
            var book = new Book("The Hobbit", "Harper", true, "Tolkien", 310, BookGenre.Fantasy);
            _data.AddItem(book);

            var reader = new Reader("Test", "User", "test.user@mail.com", "123-456-789", UserRole.Reader, 12.50);
            _data.AddUser(reader);
        }
    }
}
