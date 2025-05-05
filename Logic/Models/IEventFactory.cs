using System;

namespace Logic.Models
{
    public interface IEventFactory
    {
        IEventL CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle);
        IEventL CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle);
        IEventL CreateItemAddedEvent(Guid itemId, string itemTitle);
        IEventL CreateItemRemovedEvent(Guid itemId, string itemTitle);
        IEventL CreateUserAddedEvent(Guid userId, string userEmail);
        IEventL CreateUserRemovedEvent(Guid userId, string userEmail);
    }
}
