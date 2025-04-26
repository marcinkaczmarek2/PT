using Data.Catalog;

namespace Logic.Repositories.Interfaces
{
    public interface ILibraryRepository
    {
        void AddContent(Borrowable content);
        bool RemoveContent(Guid id);
        Borrowable? GetContent(Guid id);
        List<Borrowable> GetAllContent();
    }
}
