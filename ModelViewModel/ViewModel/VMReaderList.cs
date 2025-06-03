using ModelViewModel.Model.API;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModelViewModel.ViewModel
{
    internal class VMReaderList : PropertyChange
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _email;
        private string _phoneNumber;
        private string _role;
        private decimal _debt;

        private VMReader _selectedViewModel;
        private IReaderModelData _selectedReader;
        private IModel _iModel;

        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        private ObservableCollection<VMReader> ReaderVM;

        public VMReaderList()
        {
            _iModel = IModel.CreateNewModel();
            ReaderVM = new ObservableCollection<VMReader>();

            AddCommand = new RelayCommand(e => { Add(); }, a => true);
            DeleteCommand = new RelayCommand(e => { Delete(); }, a => true);
            RefreshCommand = new RelayCommand(e => { GetReaders(); }, a => true);
        }

        public VMReaderList(IModel model)
        {
            _iModel = model;
            ReaderVM = new ObservableCollection<VMReader>();

            AddCommand = new RelayCommand(e => { Add(); }, a => true);
            DeleteCommand = new RelayCommand(e => { Delete(); }, a => true);
            RefreshCommand = new RelayCommand(e => { GetReaders(); }, a => true);
        }

        public ObservableCollection<VMReader> ReaderView
        {
            get => ReaderVM;
            set
            {
                ReaderVM = value;
                OnPropertyChanged(nameof(ReaderView));
            }
        }

        public IReaderModelData SelectedReader
        {
            get => _selectedReader;
            set
            {
                _selectedReader = value;
                OnPropertyChanged(nameof(SelectedReader));
                _selectedViewModel = new VMReader(value.id, value.name, value.surname, value.email, value.phoneNumber, value.role, value.debt);
            }
        }

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string PhoneNumber
        {
            get => _phoneNumber;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        public decimal Debt
        {
            get => _debt;
            set
            {
                _debt = value;
                OnPropertyChanged(nameof(Debt));
            }
        }

        private VMReader? ReaderToPresentation(IReaderModelData reader)
        {
            return reader == null ? null : new VMReader(reader.id, reader.name, reader.surname, reader.email, reader.phoneNumber, reader.role, reader.debt);
        }

        public void GetReaders()
        {
            ReaderVM.Clear();

            foreach (var r in _iModel.GetAllUsers())
            {
                ReaderVM.Add(ReaderToPresentation(r));
            }

            OnPropertyChanged(nameof(ReaderView));
        }

        private async Task Add()
        {
            await _iModel.AddUser(_selectedViewModel.Id, _selectedViewModel.Name, _selectedViewModel.Surname, _selectedViewModel.Email, _selectedViewModel.PhoneNumber, _selectedViewModel.Role, _selectedViewModel.Debt);
        }

        private async Task Delete()
        {
            await _iModel.RemoveUser(_selectedViewModel.Id);
        }
    }
}
