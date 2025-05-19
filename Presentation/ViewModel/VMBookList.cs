using Presentation.Model.API;
using Presentation.Model.Implementation;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    internal class VMBookList : PropertyChange
    {
        private int id;
        private string title;
        private string publisher;
        private string author;
        private int numberOfPages;
        private string genre;

        private VMBook selectedViewModel;
        private IBookModelData selectedBook;
        private IModel _iModel;

        public ICommand AddBook { get; }
        public ICommand DeleteBook { get; }
        public ICommand Refresh { get; }

        private ObservableCollection<VMBook> _bookVM;

        public VMBookList()
        {
            _iModel = new ModelDefault();
            _bookVM = new ObservableCollection<VMBook>();
            AddBook = new RelayCommand(e => { Add(); }, a => true);
            DeleteBook = new RelayCommand(e => { Delete(); }, a => true);
            Refresh = new RelayCommand(e => { GetBooks(); }, a => true);
        }

        public VMBookList(IModel model)
        {
            _iModel = model;
            _bookVM = new ObservableCollection<VMBook>();
            AddBook = new RelayCommand(e => { Add(); }, a => true);
            DeleteBook = new RelayCommand(e => { Delete(); }, a => true);
            Refresh = new RelayCommand(e => { GetBooks(); }, a => true);
        }

        public ObservableCollection<VMBook> SelectedVM
        {
            get => _bookVM;
            set
            {
                _bookVM = value;
                OnPropertyChanged(nameof(SelectedVM));
            }
        }

        public IBookModelData SelectedBook
        {
            get => selectedBook;
            set
            {
                selectedBook = value;
                OnPropertyChanged(nameof(SelectedBook));
                selectedViewModel = new VMBook(
                    value.id,
                    value.title,
                    value.publisher,
                    value.author,
                    value.numberOfPages,
                    value.genre
                );
            }
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

        public string Title
        {
            get => title;
            set
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Publisher
        {
            get => publisher;
            set
            {
                publisher = value;
                OnPropertyChanged(nameof(Publisher));
            }
        }

        public string Author
        {
            get => author;
            set
            {
                author = value;
                OnPropertyChanged(nameof(Author));
            }
        }

        public int NumberOfPages
        {
            get => numberOfPages;
            set
            {
                numberOfPages = value;
                OnPropertyChanged(nameof(NumberOfPages));
            }
        }

        public string Genre
        {
            get => genre;
            set
            {
                genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        private VMBook? BookToPresentation(IBookModelData b)
        {
            return b == null ? null : new VMBook(b.id, b.title, b.publisher, b.author, b.numberOfPages, b.genre);
        }

        public void GetBooks()
        {
            _bookVM.Clear();

            foreach (var b in _iModel.GetAllBooks())
            {
                _bookVM.Add(BookToPresentation(b));
            }

            OnPropertyChanged(nameof(SelectedVM));
        }

        private async Task Add()
        {
            await _iModel.AddBook(
                selectedViewModel.Id,
                selectedViewModel.Title,
                selectedViewModel.Publisher,
                selectedViewModel.Author,
                selectedViewModel.NumberOfPages,
                selectedViewModel.Genre
            );
        }

        private async Task Delete()
        {
            await _iModel.RemoveBook(selectedViewModel.Id);
        }
    }
}
