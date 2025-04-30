using Data.API;
using Data.Implementations;
using Data.Catalog;
using Data.Events;
using Data.Users;

namespace DataTest.TestDataGeneration
{

    public interface IDataGenerator
    {
        IData GetDataContext();
    }
    internal class RandomDataGenerator : IDataGenerator
    {
        private readonly Random random = new(DateTime.Now.Millisecond);

        private InMemoryDataContext _context;

        public RandomDataGenerator()
        {
            _context = new InMemoryDataContext();

            GenerateItems();
            GenerateUsers();
            GenerateEvents();
        }

        public IData GetDataContext()
        {
            return _context;
        }

        private void GenerateItems()
        {
            string[] bookTitles = {
                "The Lost Kingdom", "Hidden Treasures", "Mystery of the Ancients",
                "Journey Beyond Stars", "The Silent Forest", "Legends Unfolded"
            };
            string[] publishers = { "AlphaBooks", "NovaPublish", "GalaxyPress", "EchoEditions", "ZenithBooks" };
            BookGenre[] bookGenres = (BookGenre[])Enum.GetValues(typeof(BookGenre));

            for (int i = 0; i < 10; i++)
            {
                var book = new Book(
                    title: bookTitles[random.Next(bookTitles.Length)],
                    publisher: publishers[random.Next(publishers.Length)],
                    availbility: true,
                    author: "Author" + random.Next(100),
                    numberOfPages: random.Next(100, 600),
                    genre: bookGenres[random.Next(bookGenres.Length)]
                );

                _context.AddItem(book);
            }

            GameGenre[] gameGenres = (GameGenre[])Enum.GetValues(typeof(GameGenre));
            for (int i = 0; i < 10; i++)
            {
                var boardGame = new BoardGame(
                    title: "Game" + random.Next(1000),
                    publisher: publishers[random.Next(publishers.Length)],
                    availbility: true,
                    minimumNumberOfPlayers: random.Next(1, 4),
                    maximumNumberOfPlayers: random.Next(4, 10),
                    recommendedAge: random.Next(6, 18),
                    genre: gameGenres[random.Next(gameGenres.Length)]
                );

                _context.AddItem(boardGame);
            }
        }

        private void GenerateUsers()
        {
            string[] firstNames = { "John", "Emma", "Michael", "Sophia", "William", "Olivia" };
            string[] lastNames = { "Smith", "Johnson", "Williams", "Brown", "Jones", "Davis" };
            string[] domains = { "gmail.com", "yahoo.com", "hotmail.com" };

            for (int i = 0; i < 10; i++)
            {
                var reader = new Reader(
                    name: firstNames[random.Next(firstNames.Length)],
                    surname: lastNames[random.Next(lastNames.Length)],
                    email: GenerateRandomEmail(firstNames[random.Next(firstNames.Length)], lastNames[random.Next(lastNames.Length)], domains[random.Next(domains.Length)]),
                    phoneNumber: GenerateRandomPhoneNumber(),
                    role: UserRole.Reader,
                    debt: Math.Round(random.NextDouble() * 50, 2)
                );

                _context.AddUser(reader);

                var employee = new Employee(
                    name: firstNames[random.Next(firstNames.Length)],
                    surname: lastNames[random.Next(lastNames.Length)],
                    email: GenerateRandomEmail(firstNames[random.Next(firstNames.Length)], lastNames[random.Next(lastNames.Length)], domains[random.Next(domains.Length)]),
                    phoneNumber: GenerateRandomPhoneNumber(),
                    role: UserRole.Admin,
                    salary: random.Next(3000, 8000)
                );

                _context.AddUser(employee);
            }
        }

        private void GenerateEvents()
        {
            List<User> users = _context.GetUsers();
            List<Borrowable> items = _context.GetItems();

            for (int i = 0; i < 10; i++)
            {
                var user = users[random.Next(users.Count)];
                var item = items[random.Next(items.Count)];

                var borrowDate = DateTime.Now.AddDays(-random.Next(30)).ToString("yyyy-MM-dd");

                var eventBase = new ItemBorrowedEvent(user.id, item.id, borrowDate);
                _context.AddEvent(eventBase);

                var returnDate = DateTime.Now.AddDays(-random.Next(5)).ToString("yyyy-MM-dd");

                var returnEvent = new ItemReturnedEvent(user.id, item.id, returnDate);
                _context.AddEvent(returnEvent);
            }
        }

        private static string GenerateRandomEmail(string firstName, string lastName, string domain)
        {
            return $"{firstName.ToLower()}.{lastName.ToLower()}@{domain}";
        }

        private static string GenerateRandomPhoneNumber()
        {
            Random random = new();
            string phoneNumber = "+";
            for (int i = 0; i < 9; i++)
            {
                phoneNumber += random.Next(0, 10).ToString();
            }
            return phoneNumber;
        }
    }
}
