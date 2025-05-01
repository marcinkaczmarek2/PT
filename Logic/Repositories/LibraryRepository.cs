using Data.API;
using Data.API.Models;
using Logic.Repositories.Interfaces;

namespace Logic.Repositories
{
    internal class LibraryRepository : ILibraryRepository
    {
        private readonly IData context;

        public LibraryRepository(IData context)
        {
            this.context = context;
        }

        public void AddContent(IBorrowable content)
        {
            this.context.AddItem(content);
        }

        public bool RemoveContent(Guid id)
        {
            return this.context.DeleteItem(id);
        }

        public IBorrowable? GetContent(Guid id)
        {
            return this.context.GetItem(id);
        }

        public List<IBorrowable> GetAllContent()
        {
            return this.context.GetItems();
        }
    }
}
