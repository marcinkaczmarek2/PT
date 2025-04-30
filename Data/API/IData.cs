using Data.Catalog;
using Data.Events;
using Data.Users;

namespace Data.API
{
    public interface IData
    {
        // Users
        User? GetUser(Guid id);
        List<User> GetUsers();
        void AddUser(User user);
        bool DeleteUser(Guid id);

        // Borrowables
        Borrowable? GetItem(Guid id);
        List<Borrowable> GetItems();
        void AddItem(Borrowable item);
        bool DeleteItem(Guid id);

        // Events
        List<EventBase> GetEvents();
        void AddEvent(EventBase eventBase);
    }
}
