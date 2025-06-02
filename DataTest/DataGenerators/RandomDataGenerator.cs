namespace DataTest.DataGenerators
{
    using Data.API;
    using System;

    public class RandomDataGenerator : IDataGenerator
    {
        private readonly Random _random = new Random();

        public void GenerateData(IDataRepository repository)
        {
            int bookId = _random.Next(1000, 9999);
            int readerId = _random.Next(1000, 9999);
            int stateId = _random.Next(1000, 9999);
            int eventId = _random.Next(1000, 9999);

            string[] genres = { "Fantasy", "Sci-Fi", "Drama", "Horror", "History" };
            string genre = genres[_random.Next(genres.Length)];

            repository.AddBook(bookId, $"RandomBook{bookId}", "RandomHouse", $"Author{bookId}", _random.Next(100, 600), genre);
            repository.AddReader(readerId, $"Name{readerId}", $"Surname{readerId}", $"user{readerId}@example.com", "123456789", "Guest", 0.0m);
            repository.AddState(stateId, _random.Next(1, 5), bookId);
            repository.AddEvent(eventId, readerId, bookId);
        }
    }
}
