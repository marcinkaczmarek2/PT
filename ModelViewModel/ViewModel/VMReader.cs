using System.Runtime.CompilerServices;

namespace ModelViewModel.ViewModel
{
    internal class VMReader : PropertyChange
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _email;
        private string _phoneNumber;
        private string _role;
        private decimal _debt;

        public VMReader()
        {
        }

        public VMReader(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt)
        {
            _id = id;
            _name = name;
            _surname = surname;
            _email = email;
            _phoneNumber = phoneNumber;
            _role = role;
            _debt = debt;
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
    }
}
