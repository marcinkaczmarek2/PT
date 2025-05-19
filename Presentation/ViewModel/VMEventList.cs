using Presentation.Model.API;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

[assembly: InternalsVisibleTo("Presentation.Test")]
namespace Presentation.ViewModel
{
    internal class VMEventList : PropertyChange
    {
        private int _id;
        private int _userId;
        private int _bookId;

        private VMEvent _selectedViewModel;
        private IEventModelData _selectedEvent;
        private IModel _iModel;

        public ICommand BorrowCommand { get; }
        public ICommand ReturnCommand { get; }
        public ICommand AddEventCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        private ObservableCollection<VMEvent> _eventVM;

        public VMEventList()
        {
            _iModel = IModel.CreateNewModel();
            _eventVM = new ObservableCollection<VMEvent>();

            BorrowCommand = new RelayCommand(e => { Borrow(); }, a => true);
            ReturnCommand = new RelayCommand(e => { Return(); }, a => true);
            AddEventCommand = new RelayCommand(e => { AddEvent(); }, a => true);
            DeleteCommand = new RelayCommand(e => { Delete(); }, a => true);
            RefreshCommand = new RelayCommand(e => { GetEvents(); }, a => true);
        }

        public VMEventList(IModel model)
        {
            _iModel = model;
            _eventVM = new ObservableCollection<VMEvent>();

            BorrowCommand = new RelayCommand(e => { Borrow(); }, a => true);
            ReturnCommand = new RelayCommand(e => { Return(); }, a => true);
            AddEventCommand = new RelayCommand(e => { AddEvent(); }, a => true);
            DeleteCommand = new RelayCommand(e => { Delete(); }, a => true);
            RefreshCommand = new RelayCommand(e => { GetEvents(); }, a => true);
        }

        public ObservableCollection<VMEvent> SelectedVM
        {
            get => _eventVM;
            set
            {
                _eventVM = value;
                OnPropertyChanged(nameof(SelectedVM));
            }
        }

        public IEventModelData SelectedEvent
        {
            get => _selectedEvent;
            set
            {
                _selectedEvent = value;
                OnPropertyChanged(nameof(SelectedEvent));
                _selectedViewModel = new VMEvent(value.eventId, value.userId, value.bookId);
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

        public int UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        public int BookId
        {
            get => _bookId;
            set
            {
                _bookId = value;
                OnPropertyChanged(nameof(BookId));
            }
        }

        private VMEvent? EventToPresentation(IEventModelData e)
        {
            return e == null ? null : new VMEvent(e.eventId, e.userId, e.bookId);
        }

        public void GetEvents()
        {
            _eventVM.Clear();

            foreach (var e in _iModel.GetAllEvents())
            {
                _eventVM.Add(EventToPresentation(e));
            }

            OnPropertyChanged(nameof(SelectedVM));
        }

        private async Task Borrow()
        {
            await _iModel.BorrowBook(_selectedViewModel.Id, _selectedViewModel.UserId, _selectedViewModel.BookId);
        }

        private async Task Return()
        {
            await _iModel.ReturnBook(_selectedViewModel.Id, _selectedViewModel.UserId, _selectedViewModel.BookId);
        }

        private async Task AddEvent()
        {
            await _iModel.AddEvent(_selectedViewModel.Id, _selectedViewModel.UserId, _selectedViewModel.BookId);
        }

        private async Task Delete()
        {
            await _iModel.RemoveEvent(_selectedViewModel.Id);
        }
    }
}
