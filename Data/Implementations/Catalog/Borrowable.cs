using Data.API.Models;

namespace Data.Implementations.Catalog
{
    internal abstract class Borrowable : IBorrowable
    {
        public Guid id {  get; }
        public string title {  set; get; }
        public string publisher {  set; get; }
        public bool availability {  set; get; }

        internal Borrowable(string title, string publisher, bool availbility)
        {
            id = Guid.NewGuid();
            this.title = title;
            this.publisher = publisher;
            availability = availbility;
        }
    }
    

}
