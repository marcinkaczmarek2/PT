using Data.API;
using Data.API.Models;

namespace Data.Implementations
{
    internal sealed class InMemoryDataContext : IData
    {
        private Dictionary<Guid, IUser> users = new();
        private Dictionary<Guid, IBorrowable> items = new();
        private List<IEvent> events = new();

        // Users 
        public IUser? GetUser(Guid id)
        {
            return users.GetValueOrDefault(id);
        }

        public List<IUser> GetUsers()
        {
            return users.Values.ToList();
        }

        public void AddUser(IUser user)
        {
            users[user.id] = user;
        }

        public bool DeleteUser(Guid id)
        {
            return users.Remove(id);
        }

        // Borrowables
        public IBorrowable? GetItem(Guid id)
        {
            return items.GetValueOrDefault(id);
        }

        public List<IBorrowable> GetItems()
        {
            return items.Values.ToList();
        }

        public void AddItem(IBorrowable item)
        {
            items[item.id] = item;
        }

        public bool DeleteItem(Guid id)
        {
            return items.Remove(id);
        }

        // Events
        public List<IEvent> GetEvents()
        {
            return events.ToList();
        }

        public void AddEvent(IEvent eventBase)
        {
            events.Add(eventBase);
        }
    }
}
