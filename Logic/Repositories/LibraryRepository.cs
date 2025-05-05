using Logic.Models;
using Logic.Repositories.Interfaces;
using System.Collections.Generic;

namespace Logic.Repositories
{
    public class LibraryRepository : ILibraryRepository
    {
        private readonly List<IBorrowableL> items = new();

        public void AddContent(IBorrowableL item)
        {
            items.Add(item);
        }

        public IBorrowableL? GetContent(Guid id)
        {
            return items.Find(i => i.Id == id);
        }

        public List<IBorrowableL> GetAllContent()
        {
            return new List<IBorrowableL>(items);
        }

        public bool RemoveContent(Guid id)
        {
            return items.RemoveAll(i => i.Id == id) > 0;
        }
    }
}
