using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Presentation.Model.Implementation;

namespace Presentation.ViewModel
{
    public class VMCatalogList : INotifyPropertyChanged
    {
        public ObservableCollection<BookModelData> Books { get; set; } = new();
        private BookModelData _selectedBook;
        public BookModelData SelectedBook
        {
            get => _selectedBook;
            set { _selectedBook = value; OnPropertyChanged(nameof(SelectedBook)); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public VMCatalogList()
        {
            LoadMockBooks();

            AddCommand = new RelayCommand(_ => Books.Add(new BookModelData { Id = Books.Count + 1, Title = "New", Author = "Unknown" }));
            DeleteCommand = new RelayCommand(_ => Books.Remove(SelectedBook), _ => SelectedBook != null);
            RefreshCommand = new RelayCommand(_ => LoadMockBooks());
        }

        private void LoadMockBooks()
        {
            Books.Clear();
            Books.Add(new BookModelData { Id = 1, Title = "C# Basics", Author = "John Doe" });
            Books.Add(new BookModelData { Id = 2, Title = "Advanced .NET", Author = "Jane Smith" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
