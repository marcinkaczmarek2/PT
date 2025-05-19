namespace Data.API
{
    public interface IEvent
    {
        int eventId { get; }
        int userId { get; }
        int bookId { get;  }
    }
}
