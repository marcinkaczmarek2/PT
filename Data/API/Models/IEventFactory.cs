namespace Data.API.Models
{
    public interface IEventFactory
    {
        IEvent CreateItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle);
        IEvent CreateItemReturnedEvent(Guid userId, Guid itemId, string itemTitle);
        IEvent CreateItemAddedEvent(Guid itemId, string itemTitle);
        IEvent CreateItemRemovedEvent(Guid itemId, string itemTitle);
        IEvent CreateUserAddedEvent(Guid userId, string userEmail);
        IEvent CreateUserRemovedEvent(Guid userId, string userEmail);
    }
}
