
namespace Data.API
{
    public interface IState
    {
        int bookId { get; }
        int stateId { get; set; }
        int quantity { get; set; }
    }
}
