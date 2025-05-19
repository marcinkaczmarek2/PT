using Services.API;

namespace Services.Implementation
{
    internal class ReaderServiceData : IReaderServiceData
    {
        public ReaderServiceData(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.role = role;
            this.debt = debt;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string role { get; set; }
        public decimal debt { get; set; }
    }
}
