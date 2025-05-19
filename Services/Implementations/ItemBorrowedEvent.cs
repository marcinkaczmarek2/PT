using Data.API;

namespace Services.Implementation
{
    internal class ItemBorrowedEvent : ItemBorrowedEventI
    {
        private IState _state;
        private IUser _user;

        public int id { get; set; }

        public int eventId => id;
        public int userId => _user.id;
        public int bookId => _state.bookId;

        public int quantityChanged { get; set; }

        public ItemBorrowedEvent(IState state, IUser user, int quantityChanged)
        {
            _state = state;
            _user = user;
            this.quantityChanged = quantityChanged;
        }
    }
}
