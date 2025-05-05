using Data.API.Models;
using Data.Events;

namespace Data.Factories
{
    internal class EventFactory : IEventFactory
    {
        public IEventD CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle)
        {
            return new ItemBorrowedEvent(userId, itemId, itemTitle);
        }

        public IEventD CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle)
        {
            return new ItemReturnedEvent(userId, itemId, itemTitle);
        }

        public IEventD CreateItemAddedEvent(Guid itemId, string itemTitle)
        {
            return new ItemAddedEvent(itemId, itemTitle);
        }

        public IEventD CreateItemRemovedEvent(Guid itemId, string itemTitle)
        {
            return new ItemRemovedEvent(itemId, itemTitle);
        }

        public IEventD CreateUserAddedEvent(Guid userId, string userEmail)
        {
            return new UserAddedEvent(userId, userEmail);
        }

        public IEventD CreateUserRemovedEvent(Guid userId, string userEmail)
        {
            return new UserRemovedEvent(userId, userEmail);
        }
    }
}