namespace Logic.Services.Interfaces
{
    public interface IBorrowService
    {
        bool BorrowItem(Guid userId, Guid itemId);
        bool ReturnItem(Guid userId, Guid itemId);
    }

}
