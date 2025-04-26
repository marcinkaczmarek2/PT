
namespace Data.Events
{
    public class ItemReturnedEvent : EventBase
    {
        public Guid userId { get; set; }
        public Guid itemId { get; set; }

        public string itemTitle { get; set; }

        public ItemReturnedEvent(Guid userId, Guid itemId, string itemTitle)
            : base()
        {
            this.userId = userId;
            this.itemId = itemId;
            this.itemTitle = itemTitle;
        }

        public override string ToString()
        {
            return $"[ItemReturnedEvent] UserId: {userId}, BookId: {itemId}, ItemTitle: {itemTitle} , Timestamp: {timestamp}";
        }
    }
}