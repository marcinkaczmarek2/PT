namespace Data.API.Models
{
    public interface IEventFactory
    {
        IEventD CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle);
        IEventD CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle);
        IEventD CreateItemAddedEvent(Guid itemId, string itemTitle);
        IEventD CreateItemRemovedEvent(Guid itemId, string itemTitle);
        IEventD CreateUserAddedEvent(Guid userId, string userEmail);
        IEventD CreateUserRemovedEvent(Guid userId, string userEmail);
    }
}
