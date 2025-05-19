using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Presentation.Model.Implementation;

namespace Presentation.ViewModel
{
    public class VMStateList : INotifyPropertyChanged
    {
        public ObservableCollection<StateModelData> Items { get; set; } = new();
        private StateModelData _selectedItem;
        public StateModelData SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public VMStateList()
        {
            LoadMock();

            AddCommand = new RelayCommand(_ => Items.Add(new StateModelData()));
            DeleteCommand = new RelayCommand(_ => Items.Remove(SelectedItem), _ => SelectedItem != null);
            RefreshCommand = new RelayCommand(_ => LoadMock());
        }

        private void LoadMock()
        {
            Items.Clear();
            Items.Add(new StateModelData { Id = 1, Status = "State Item 1" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
