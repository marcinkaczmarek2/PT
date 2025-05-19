using Data.API;

namespace ServicesTest
{
    internal class MockState : IState
    {
        public int stateId { get; set; }
        public int quantity { get; set; }
        public int bookId { get; set; }

        public MockState(int stateId, int bookId, int quantity)
        {
            this.stateId = stateId;
            this.bookId = bookId;
            this.quantity = quantity;
        }
    }
}
