using System;

namespace Logic.Models
{
    public interface IBorrowableL
    {
        Guid Id { get; }
        string Title { get; set; }
        string Publisher { get; set; }
        bool availability { get; set; }
    }
}
