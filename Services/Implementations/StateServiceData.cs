using Services.API;

namespace Services.Implementation
{
    internal class StateServiceData : IStateServiceData
    {
        public StateServiceData(int stateId, int bookId, int quantity)
        {
            this.stateId = stateId;
            this.bookId = bookId;
            this.quantity = quantity;
        }

        public int stateId { get; set; }
        public int bookId { get; set; }
        public int quantity { get; set; }
    }
}
