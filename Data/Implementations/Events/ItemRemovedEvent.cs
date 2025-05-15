namespace Data.Implementations.Events
{
    internal class ItemRemovedEvent : EventBase
    {
        internal Guid itemId { get; private set; }
        internal string? itemTitle { get; private set; }

        internal ItemRemovedEvent(Guid itemId, string itemTitle)
            : base()
        {
            this.itemId = itemId;
            this.itemTitle = itemTitle;
        }

        public override string ToString()
        {
            return $"[ItemRemovedEvent] ItemId: {itemId}, ItemTitle: {itemTitle}, Timestamp: {timestamp}";
        }
    }
}
