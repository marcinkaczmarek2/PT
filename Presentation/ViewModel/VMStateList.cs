using Presentation.Model.API;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Presentation.Test")]
namespace Presentation.ViewModel
{
    internal class VMStateList : PropertyChange
    {
        private IModel _iModel;

        private ObservableCollection<VMState> _stateVM;

        public VMStateList()
        {
            _iModel = IModel.CreateNewModel();
            _stateVM = new ObservableCollection<VMState>();
        }

        public VMStateList(IModel? model)
        {
            _iModel = model ?? IModel.CreateNewModel();
            _stateVM = new ObservableCollection<VMState>();
        }

        public ObservableCollection<VMState> StateView
        {
            get => _stateVM;
            set
            {
                _stateVM = value;
                OnPropertyChanged(nameof(StateView));
            }
        }

        public void GetStates()
        {
            StateView.Clear();

            foreach (var state in _iModel.GetAllStates())
            {
                StateView.Add(StateToPresentation(state));
            }

            OnPropertyChanged(nameof(StateView));
        }

        private VMState StateToPresentation(IStateModelData state)
        {
            return new VMState(state.stateId, state.bookId, state.quantity);
        }
    }
}
