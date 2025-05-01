using Data.API.Models;

namespace Logic.Services.Interfaces
{
    public interface ILibraryService
    {
        bool AddContent(IBorrowable content);
        bool RemoveContent(Guid id);
        IBorrowable? GetContent(Guid id);
        List<IBorrowable> GetAllContent();
    }

}
