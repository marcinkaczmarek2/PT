using System.Windows.Input;

namespace ModelViewModel.ViewModel
{
    internal class VMBook : PropertyChange
    {
        private int _id;
        private string _title;
        private string _publisher;
        private string _author;
        private int _numberOfPages;
        private string _genre;

        public ICommand AddBook { get; }
        public ICommand DeleteBook { get; }

        public VMBook(int id, string title, string publisher, string author, int numberOfPages, string genre)
        {
            _id = id;
            _title = title;
            _publisher = publisher;
            _author = author;
            _numberOfPages = numberOfPages;
            _genre = genre;

        }

        public VMBook() : this(0, "Sample Title", "Sample Publisher", "Sample Author", 0, "Sample Genre") { }

        public int Id
        {
            get => _id;
            set
            {
                if (SetProperty(ref _id, value))
                    OnPropertyChanged(nameof(Id));
            }
        }

        public string Title
        {
            get => _title;
            set
            {
                if (SetProperty(ref _title, value))
                    OnPropertyChanged(nameof(Title));
            }
        }

        public string Publisher
        {
            get => _publisher;
            set
            {
                if (SetProperty(ref _publisher, value))
                    OnPropertyChanged(nameof(Publisher));
            }
        }

        public string Author
        {
            get => _author;
            set
            {
                if (SetProperty(ref _author, value))
                    OnPropertyChanged(nameof(Author));
            }
        }

        public int NumberOfPages
        {
            get => _numberOfPages;
            set
            {
                if (SetProperty(ref _numberOfPages, value))
                    OnPropertyChanged(nameof(NumberOfPages));
            }
        }

        public string Genre
        {
            get => _genre;
            set
            {
                if (SetProperty(ref _genre, value))
                    OnPropertyChanged(nameof(Genre));
            }
        }
    }
}
