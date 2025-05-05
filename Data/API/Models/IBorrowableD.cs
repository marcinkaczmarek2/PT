namespace Data.API.Models
{
    public interface IBorrowableD
    {
        Guid id { get; }
        string title { get; set; }
        string publisher { get; set; }
        bool availability { get; set; }
    }
}