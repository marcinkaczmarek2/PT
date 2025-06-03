using ModelViewModel.Model.API;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ModelViewModel.ViewModel
{
    internal class VMState : PropertyChange
    {
        private int _stateId;
        private int _bookId;
        private int _quantity;

        private readonly IModel _model;

        public ICommand AddState { get; }
        public ICommand DeleteState { get; }

        public VMState()
        {
            _model = IModel.CreateNewModel();

            AddState = new RelayCommand(e => { _ = Add(); }, a => true);
            DeleteState = new RelayCommand(e => { _ = Delete(); }, a => true);
        }

        public VMState(IModel model)
        {
            _model = model;

            AddState = new RelayCommand(e => { _ = Add(); }, a => true);
            DeleteState = new RelayCommand(e => { _ = Delete(); }, a => true);
        }

        public VMState(int stateId, int bookId, int quantity)
        {
            _stateId = stateId;
            _bookId = bookId;
            _quantity = quantity;
            _model = IModel.CreateNewModel();

            AddState = new RelayCommand(e => { _ = Add(); }, a => true);
            DeleteState = new RelayCommand(e => { _ = Delete(); }, a => true);
        }

        public int StateId
        {
            get => _stateId;
            set
            {
                _stateId = value;
                OnPropertyChanged(nameof(StateId));
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

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private async Task Add()
        {
            await _model.AddState(StateId, BookId, Quantity);
        }

        private async Task Delete()
        {
            await _model.RemoveState(StateId);
        }
    }
}
