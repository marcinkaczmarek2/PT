using Data.API.Models;
using Logic.Repositories.Interfaces;

namespace Logic.Repositories.Implementations
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly List<IBorrowable> items = new();

        public void AddContent(IBorrowable item)
        {
            items.Add(item);
        }

        public IBorrowable? GetContent(Guid id)
        {
            return items.Find(i => i.id == id);
        }

        public List<IBorrowable> GetAllContent()
        {
            return new List<IBorrowable>(items);
        }

        public bool RemoveContent(Guid id)
        {
            return items.RemoveAll(i => i.id == id) > 0;
        }
    }
}
