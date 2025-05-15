namespace Data.Implementations.Events
{
    internal class ItemReturnedEvent : EventBase
    {
        internal Guid userId { get; private set; }
        internal Guid itemId { get; private set; }

        internal string itemTitle { get; private set; }

        internal ItemReturnedEvent(Guid userId, Guid itemId, string itemTitle)
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