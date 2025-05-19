using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Presentation.Model.Implementation;

namespace Presentation.ViewModel
{
    public class VMUserList : INotifyPropertyChanged
    {
        public ObservableCollection<UserModelData> Items { get; set; } = new();
        private UserModelData _selectedItem;
        public UserModelData SelectedItem
        {
            get => _selectedItem;
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        public VMUserList()
        {
            LoadMock();

            AddCommand = new RelayCommand(_ => Items.Add(new UserModelData()));
            DeleteCommand = new RelayCommand(_ => Items.Remove(SelectedItem), _ => SelectedItem != null);
            RefreshCommand = new RelayCommand(_ => LoadMock());
        }

        private void LoadMock()
        {
            Items.Clear();
            Items.Add(new UserModelData { Id = 1, Name = "User Item 1" });
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
