
namespace Data.Events
{
    internal class ItemAddedEvent : EventBase
    {
        internal Guid itemId { get; private set; }
        internal string? itemTitle { get; private set; }

        internal ItemAddedEvent(Guid itemId, string itemTitle)
            : base()
        {
            this.itemId = itemId;
            this.itemTitle = itemTitle;
        }

        public override string ToString()
        {
            return $"[ItemAddedEvent] ProductId: {itemId}, ProductName: {itemTitle}, Timestamp: {timestamp}";
        }
    }
}
