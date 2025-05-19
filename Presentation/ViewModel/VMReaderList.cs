using Presentation.Model.Implementation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class VMReaderList : INotifyPropertyChanged
    {
        public ObservableCollection<ReaderModelData> Readers { get; set; } = new();
        public ReaderModelData SelectedReader { get; set; }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        public VMReaderList()
        {
            Readers.Add(new ReaderModelData { Id = 1, Name = "Alice", Email = "alice@example.com" });
            AddCommand = new RelayCommand(_ => Readers.Add(new ReaderModelData { Name = "New", Email = "none@example.com" }));
            RemoveCommand = new RelayCommand(_ => Readers.Remove(SelectedReader), _ => SelectedReader != null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
