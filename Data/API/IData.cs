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
        IBorrowableD? GetItem(Guid id);
        List<IBorrowableD> GetItems();
        void AddItem(IBorrowableD item);
        bool DeleteItem(Guid id);

        // Events
        List<IEventD> GetEvents();
        void AddEvent(IEventD eventBase);
    }
}
