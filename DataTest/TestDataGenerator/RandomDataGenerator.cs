using Data.API;
using Data.API.Models;
using Data.Catalog;
using Data.Enums;
using Data.Users;
using Data.Implementations;

namespace DataLayerTest.TestDataGeneration
{
    internal class RandomDataGenerator : IDataGenerator
    {
        private readonly IData _data;
        private readonly Random _random = new();

        public RandomDataGenerator()
        {
            _data = new InMemoryDataContext();
            GenerateBooks();
            GenerateUsers();
        }

        public IData GetData() => _data;

        private void GenerateBooks()
        {
            string[] titles = { "1984", "Brave New World", "The Hobbit", "Dune" };
            string[] publishers = { "Penguin", "Harper", "Tor" };
            string[] authors = { "Orwell", "Huxley", "Tolkien", "Herbert" };
            BookGenre[] genres = (BookGenre[])Enum.GetValues(typeof(BookGenre));

            for (int i = 0; i < 10; i++)
            {
                var book = new Book(
                    title: titles[_random.Next(titles.Length)],
                    publisher: publishers[_random.Next(publishers.Length)],
                    availbility: _random.NextDouble() > 0.3,
                    author: authors[_random.Next(authors.Length)],
                    numberOfPages: _random.Next(100, 600),
                    genre: genres[_random.Next(genres.Length)]
                );

                _data.AddItem(book);
            }
        }

        private void GenerateUsers()
        {
            string[] names = { "Alice", "Bob", "Charlie", "Dana" };
            string[] surnames = { "Smith", "Johnson", "Lee", "Brown" };
            string[] domains = { "example.com", "mail.com" };
            string[] phones = { "123-456-789", "555-123-456", "987-654-321", "111-222-333" };

            for (int i = 0; i < 5; i++)
            {
                string name = names[_random.Next(names.Length)];
                string surname = surnames[_random.Next(surnames.Length)];
                string email = $"{name.ToLower()}.{surname.ToLower()}@{domains[_random.Next(domains.Length)]}";
                string phone = phones[_random.Next(phones.Length)];
                double debt = Math.Round(_random.NextDouble() * 100, 2);

                var reader = new Reader(name, surname, email, phone, UserRole.Reader, debt);
                _data.AddUser(reader);
            }
        }

    }
}
