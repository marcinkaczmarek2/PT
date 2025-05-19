using Presentation.Model.Implementation;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Presentation.ViewModel
{
    public class VMEmployeeList : INotifyPropertyChanged
    {
        public ObservableCollection<EmployeeModelData> Employees { get; set; } = new();
        public EmployeeModelData SelectedEmployee { get; set; }

        public ICommand AddCommand { get; }
        public ICommand RemoveCommand { get; }

        public VMEmployeeList()
        {
            Employees.Add(new EmployeeModelData { Id = 1, Name = "Anna", Position = "Librarian" });
            Employees.Add(new EmployeeModelData { Id = 2, Name = "John", Position = "Admin" });

            AddCommand = new RelayCommand(_ => Employees.Add(new EmployeeModelData { Name = "New", Position = "None" }));
            RemoveCommand = new RelayCommand(_ => Employees.Remove(SelectedEmployee), _ => SelectedEmployee != null);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
