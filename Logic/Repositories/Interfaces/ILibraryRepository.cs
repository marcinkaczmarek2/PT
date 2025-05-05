using Logic.Models;

namespace Logic.Repositories.Interfaces
{
    public interface ILibraryRepository
    {
        void AddContent(IBorrowableL content);
        bool RemoveContent(Guid Id);
        IBorrowableL? GetContent(Guid Id);
        List<IBorrowableL> GetAllContent();
    }
}
