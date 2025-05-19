using Data.API;
namespace Data.Implementations
{
    internal class State : IState
    {
        public int stateId { get; set; }
        public int bookId{ get; set; }
        public int quantity { get; set; }

        public State(int stateId, int bookId, int quantity)
        {
            this.stateId = stateId;
            this.bookId = bookId;
            this.quantity = quantity;
        }
    }
}
