using System.Runtime.CompilerServices;

namespace ModelViewModel.ViewModel
{
    internal class VMEvent : PropertyChange
    {
        private int id;
        private int userId;
        private int bookId;

        public VMEvent()
        {
            id = 0;
            userId = 0;
            bookId = 0;
        }

        public VMEvent(int id, int userId, int bookId)
        {
            this.id = id;
            this.userId = userId;
            this.bookId = bookId;
        }

        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        public int BookId
        {
            get => bookId;
            set
            {
                bookId = value;
                OnPropertyChanged(nameof(BookId));
            }
        }
    }
}
