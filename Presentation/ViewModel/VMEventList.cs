using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Presentation.Model.Implementation;

namespace Presentation.ViewModel
{
    public class VMEventList : INotifyPropertyChanged
    {
        public ObservableCollection<EventModelData> Items { get; set; } = new();
        private EventModelData _selectedItem;
        public EventModelData SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public VMEventList()
        {
            LoadMock();

            AddCommand = new RelayCommand(_ => Items.Add(new EventModelData()));
            DeleteCommand = new RelayCommand(_ => Items.Remove(SelectedItem), _ => SelectedItem != null);
            RefreshCommand = new RelayCommand(_ => LoadMock());
        }

        private void LoadMock()
        {
            Items.Clear();
            Items.Add(new EventModelData { Id = 1, Description = "Event Item 1" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
