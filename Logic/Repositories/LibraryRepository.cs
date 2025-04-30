using Data.API;
using Data.Catalog;
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

        public void AddContent(Borrowable content)
        {
            this.context.AddItem(content);
        }

        public bool RemoveContent(Guid id)
        {
            return this.context.DeleteItem(id);
        }

        public Borrowable? GetContent(Guid id)
        {
            return this.context.GetItem(id);
        }

        public List<Borrowable> GetAllContent()
        {
            return this.context.GetItems();
        }
    }
}
