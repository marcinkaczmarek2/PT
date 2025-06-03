using ModelViewModel.Model.API;

namespace ModelViewModel.Model.Implementation
{
    internal class BookModelData : IBookModelData
    {
        public BookModelData(int id, string title, string publisher, string author, int numberOfPages, string genre)
        {
            id = id;
            title = title;
            publisher = publisher;
            author = author;
            numberOfPages = numberOfPages;
            genre = genre;
        }

        public int id { get; set; }
        public string title { get; set; }
        public string publisher { get; set; }
        public string author { get; set; }
        public int numberOfPages { get; set; }
        public string genre { get; set; }
    }
}
