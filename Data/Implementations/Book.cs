using Data.API;

namespace Data.Implementations
{
    internal class Book : IBook
    {
        public int id { get; set; }
        public string title { get; set; }
        public string publisher { get; set; }
        public string author { get; set; }
        public int numberOfPages { get; set; }
        public string genre { get; set; }

        public Book(int id, string title, string publisher, string author, int numberOfPages, string genre)
        {
            this.id = id;
            this.title = title;
            this.publisher = publisher;
            this.author = author;
            this.numberOfPages = numberOfPages;
            this.genre = genre;
        }
    }
}
