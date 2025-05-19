using Presentation.Model.API;
using System.ComponentModel;

namespace Presentation.Model.Implementation
{
    public class EmployeeModelData : IEmployeeModelData, INotifyPropertyChanged
    {
        private string _name;
        private string _position;

        public int Id { get; set; }

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Position
        {
            get => _position;
            set { _position = value; OnPropertyChanged(nameof(Position)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
