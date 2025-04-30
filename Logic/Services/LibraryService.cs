using Data.Catalog;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal sealed class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository libraryRepository;

        internal LibraryService(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository;
        }

        public bool AddContent(Borrowable content)
        {
            if (libraryRepository.GetContent(content.id) != null)
            {
                throw new InvalidOperationException("Error, cannot add another item with the same id.");
            }
            libraryRepository.AddContent(content);
            return true;
        }

        public bool RemoveContent(Guid id)
        {
            var existingContent = libraryRepository.GetContent(id);
            if (existingContent == null)
            {
                return false;
            }
            return libraryRepository.RemoveContent(id);
        }

        public Borrowable? GetContent(Guid id)
        {
            var receivedContent = libraryRepository.GetContent(id);
            if (receivedContent == null)
            {
                throw new InvalidOperationException("Error, no item with such id.");
            }
            return receivedContent;
        }

        public List<Borrowable> GetAllContent()
        {
            var receivedList = libraryRepository.GetAllContent();
            if (receivedList.Count == 0)
            {
                throw new InvalidOperationException("Error, no items in stock.");
            }
            return receivedList;
        }
    }
}
