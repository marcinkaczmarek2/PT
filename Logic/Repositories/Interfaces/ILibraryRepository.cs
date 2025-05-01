using Data.API.Models;

namespace Logic.Repositories.Interfaces
{
    public interface ILibraryRepository
    {
        void AddContent(IBorrowable content);
        bool RemoveContent(Guid id);
        IBorrowable? GetContent(Guid id);
        List<IBorrowable> GetAllContent();
    }
}
