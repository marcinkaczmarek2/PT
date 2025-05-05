using Logic.Models;

namespace Logic.Services.Interfaces
{
    public interface ILibraryService
    {
        bool AddContent(IBorrowableL content);
        bool RemoveContent(Guid id);
        IBorrowableL? GetContent(Guid id);
        List<IBorrowableL> GetAllContent();
    }

}
