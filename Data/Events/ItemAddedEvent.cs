
namespace Data.Events
{
    public class ItemAddedEvent : EventBase
    {
        public Guid itemId { get; set; }
        public string? itemTitle { get; set; }

        public ItemAddedEvent(Guid itemId, string itemTitle)
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
