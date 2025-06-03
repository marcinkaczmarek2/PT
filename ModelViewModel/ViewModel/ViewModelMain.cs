using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ModelViewModel.ViewModel
{
    internal class ViewModelMain : PropertyChange
    {
        private PropertyChange _selectedViewModel;

        public PropertyChange SelectedViewModel
        {
            get => _selectedViewModel;
            set
            {
                if (SetProperty(ref _selectedViewModel, value))
                {
                    OnPropertyChanged(nameof(SelectedViewModel));
                }
            }
        }

        public ICommand UpdateViewCommand { get; }

        public ViewModelMain()
        {
            UpdateViewCommand = new RelayCommand(ChangeView);
            SelectedViewModel = new VMBookList(); 
        }

        private void ChangeView(object parameter)
        {
            switch (parameter?.ToString())
            {
                case "BList":
                    SelectedViewModel = new VMBookList();
                    break;
                case "RList":
                    SelectedViewModel = new VMReaderList();
                    break;
                case "EList":
                    SelectedViewModel = new VMEventList();
                    break;
                case "SList":
                    SelectedViewModel = new VMStateList();
                    break;
            }
        }
    }
}
