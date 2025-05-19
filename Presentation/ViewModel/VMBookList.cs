using Presentation.Model.Implementation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class VMBookList : INotifyPropertyChanged
    {
        public ObservableCollection<BookModelData> Books { get; set; } = new();
        public BookModelData SelectedBook { get; set; }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        public VMBookList()
        {
            Books.Add(new BookModelData { Id = 1, Title = "C# Basics", Author = "John Doe" });
            AddCommand = new RelayCommand(_ => Books.Add(new BookModelData { Title = "New Book", Author = "Unknown" }));
            RemoveCommand = new RelayCommand(_ => Books.Remove(SelectedBook), _ => SelectedBook != null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
