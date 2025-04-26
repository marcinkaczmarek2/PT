using System;

namespace Data.Events
{
    public class ItemBorrowedEvent : EventBase
    {
        public Guid userId { get; set; }
        public Guid itemId { get; set; }

        public string itemTitle { get; set; }

        public ItemBorrowedEvent(Guid userId, Guid itemId, string itemTitle)
            : base()
        {
            this.userId = userId;
            this.itemId = itemId;
            this.itemTitle = itemTitle;
        }

        public override string ToString()
        {
            return $"[ItemBorrowedEvent] UserId: {userId}, BookId: {itemId}, ItemTitle: {itemTitle} , Timestamp: {timestamp}";
        }
    }
}