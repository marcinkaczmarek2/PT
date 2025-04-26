using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Events
{
    public class ItemRemovedEvent : EventBase
    {
        public Guid itemId { get; set; }
        public string? itemTitle { get; set; }

        public ItemRemovedEvent(Guid itemId, string itemTitle)
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
