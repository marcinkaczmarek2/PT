using Data.API.Models;

namespace Data.API
{
    public interface IData
    {
        // Users
        IUser? GetUser(Guid id);
        List<IUser> GetUsers();
        void AddUser(IUser user);
        bool DeleteUser(Guid id);

        // Borrowables
        IBorrowable? GetItem(Guid id);
        List<IBorrowable> GetItems();
        void AddItem(IBorrowable item);
        bool DeleteItem(Guid id);

        // Events
        List<IEvent> GetEvents();
        void AddEvent(IEvent eventBase);
    }
}
