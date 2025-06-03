using ModelViewModel.Model.API;

namespace ModelViewModel.Model.Implementation
{
    internal class ReaderModelData : IReaderModelData
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string role { get; set; }
        public decimal debt { get; set; }

        public ReaderModelData(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.role = role;
            this.debt = debt;
        }
    }
}
