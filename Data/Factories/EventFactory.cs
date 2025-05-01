using Data.API.Models;
using Data.Events;

namespace Data.Factories
{
    internal class EventFactory : IEventFactory
    {
        public IEvent CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle)
        {
            return new ItemBorrowedEvent(userId, itemId, itemTitle);
        }

        public IEvent CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle)
        {
            return new ItemReturnedEvent(userId, itemId, itemTitle);
        }

        public IEvent CreateItemAddedEvent(Guid itemId, string itemTitle)
        {
            return new ItemAddedEvent(itemId, itemTitle);
        }

        public IEvent CreateItemRemovedEvent(Guid itemId, string itemTitle)
        {
            return new ItemRemovedEvent(itemId, itemTitle);
        }

        public IEvent CreateUserAddedEvent(Guid userId, string userEmail)
        {
            return new UserAddedEvent(userId, userEmail);
        }

        public IEvent CreateUserRemovedEvent(Guid userId, string userEmail)
        {
            return new UserRemovedEvent(userId, userEmail);
        }
    }
}