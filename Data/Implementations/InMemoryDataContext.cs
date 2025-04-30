using Data.API;
using Data.Catalog;
using Data.Events;
using Data.Users;

namespace Data.Implementations
{
    internal sealed class InMemoryDataContext : IData
    {
        
        private Dictionary<Guid, User> users = new();
        private Dictionary<Guid, Borrowable> items = new();
        private List<EventBase> events = new();

        // Users
        public User? GetUser(Guid id) {
            return users.GetValueOrDefault(id);
        }
        public List<User> GetUsers()
        {
            return users.Values.ToList();
        }
        public void AddUser(User user)
        {
            users.Add(user.id, user);
        }
        public bool DeleteUser(Guid id)
        {
            return users.Remove(id);
        }

        // Borrowables
        public Borrowable? GetItem(Guid id)
        {
            return items.GetValueOrDefault(id);
        }
        public List<Borrowable> GetItems()
        {
            return items.Values.ToList();
        }
        public void AddItem(Borrowable item)
        {
            items.Add(item.id, item);
        }
        public bool DeleteItem(Guid id)
        {
            return items.Remove(id);
        }

        // Events
        public List<EventBase> GetEvents()
        {
            return events.ToList();
        }
        public void AddEvent(EventBase eventBase)
        {
            events.Add(eventBase);
        }
        
    }
}
