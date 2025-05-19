using Presentation.Model.Implementation;
using System.ComponentModel;

namespace Presentation.ViewModel
{
    public class VMEmployee : INotifyPropertyChanged
    {
        private EmployeeModelData _employee;
        public EmployeeModelData Employee
        {
            get => _employee;
            set { _employee = value; OnPropertyChanged(nameof(Employee)); }
        }

        public VMEmployee(EmployeeModelData employee)
        {
            Employee = employee;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
