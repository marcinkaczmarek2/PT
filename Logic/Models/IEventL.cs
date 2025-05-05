using System;

namespace Logic.Models
{
    public interface IEventL
    {
        Guid EventId { get; }
        DateTime Timestamp { get; set; }
    }
}
