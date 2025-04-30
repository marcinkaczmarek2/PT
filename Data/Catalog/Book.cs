
namespace Data.Catalog
{
    internal class Book : Borrowable
    {
        internal string author { private set; get; }
        internal int numberOfPages { private set; get; }
        internal BookGenre genre { private set; get; }
        internal Book(string title, string publisher, bool availbility, string author, int numberOfPages, BookGenre genre) 
            : base(title, publisher, availbility)
        {
            this.author = author;
            this.numberOfPages = numberOfPages;
            this.genre = genre;
        }
    }
    
    internal enum BookGenre
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
