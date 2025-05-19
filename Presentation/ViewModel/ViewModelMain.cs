using System.ComponentModel;
using System.Windows.Input;
using Presentation.View.Employee;
using Presentation.View.Book;
using Presentation.View.Reader;
using Presentation.View.Catalog;
using Presentation.View.User;
using Presentation.View.State;
using Presentation.View.Event;

namespace Presentation.ViewModel
{
    public class ViewModelMain : INotifyPropertyChanged
    {
        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        public ICommand ShowEmployeeCommand { get; }
        public ICommand ShowBookCommand { get; }
        public ICommand ShowReaderCommand { get; }
        public ICommand ShowCatalogCommand { get; }
        public ICommand ShowUserCommand { get; }
        public ICommand ShowEventCommand { get; }
        public ICommand ShowStateCommand { get; }

        public ViewModelMain()
        {
            ShowEmployeeCommand = new RelayCommand(_ => CurrentView = new EmployeeMaster());
            ShowBookCommand = new RelayCommand(_ => CurrentView = new BookMaster());
            ShowReaderCommand = new RelayCommand(_ => CurrentView = new ReaderMaster());
            ShowCatalogCommand = new RelayCommand(_ => CurrentView = new CatalogMaster());
            ShowUserCommand = new RelayCommand(_ => CurrentView = new UserMaster());
            ShowEventCommand = new RelayCommand(_ => CurrentView = new EventMaster());
            ShowStateCommand = new RelayCommand(_ => CurrentView = new StateMaster());

            // Domyślny widok startowy
            CurrentView = new CatalogMaster();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
