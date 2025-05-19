using Data.API;

namespace Data.Implementations
{
    internal class Reader : IUser
    {
        public int id { set; get; }
        public string name { set; get; }
        public string surname { set; get; }
        public string email { set; get; }
        public string phoneNumber { set; get; }
        public string role { set; get; }
        public decimal debt { private set; get; }
        public Reader(int id, string name, string surname, string email, string phoneNumber, string role, decimal debt)
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
