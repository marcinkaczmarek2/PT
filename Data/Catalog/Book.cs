
namespace Data.Catalog
{
    public class Book : Borrowable
    {
        public string author { set; get; }
        public int numberOfPages { set; get; }
        public BookGenre genre { set; get; }
        public Book(string title, string publisher, bool availbility, string author, int numberOfPages, BookGenre genre) 
            : base(title, publisher, availbility)
        {
            this.author = author;
            this.numberOfPages = numberOfPages;
            this.genre = genre;
        }
    }
    
    public enum BookGenre
    {
        Fantasy,
        ScienceFiction,
        Romance,
        Mystery,
        Thriller,
        Horror,
        Biography,
        History,
        Science,
        Philosophy,
        Poetry,
        Drama,
        Adventure,
        SelfImprovement,
        Business,
        Health,
        Religion,
        Travel,
        Art,
        Comic,
        Children,
        YoungAdult
    }
}
