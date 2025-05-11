using Data.API.Models;

namespace Logic.Repositories.Interfaces
{
    public interface ILibraryRepository
    {
        void AddContent(IBorrowable content);
        bool RemoveContent(Guid Id);
        IBorrowable? GetContent(Guid Id);
        List<IBorrowable> GetAllContent();
    }
}
