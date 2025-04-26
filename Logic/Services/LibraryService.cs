using Data.Catalog;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository libraryRepository;

        public LibraryService(ILibraryRepository libraryRepository)
        {
            this.libraryRepository = libraryRepository;
        }

        public bool AddContent(Borrowable content)
        {
            if (libraryRepository.GetContent(content.id) != null)
            {
                throw new Exception("Error, cannot add another item with the same id.");
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
                throw new Exception("Error, no item with such id.");
            }
            return receivedContent;
        }

        public List<Borrowable> GetAllContent()
        {
            var receivedList = libraryRepository.GetAllContent();
            if (receivedList.Count == 0)
            {
                throw new Exception("Error, no items in stock.");
            }
            return receivedList;
        }
    }
}
