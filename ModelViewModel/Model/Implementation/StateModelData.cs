using ModelViewModel.Model.API;

namespace ModelViewModel.Model.Implementation
{
    internal class StateModelData : IStateModelData
    {
        public int stateId { get; set; }
        public int bookId { get; set; }
        public int quantity { get; set; }

        public StateModelData(int stateId, int bookId, int quantity)
        {
            this.stateId = stateId;
            this.bookId = bookId;
            this.quantity = quantity;
        }
    }
}
