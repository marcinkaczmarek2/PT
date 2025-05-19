using Data.API;

namespace ServicesTest
{
    internal class MockReader : IUser
    {
        public int id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
        public string role { get; set; }

        public MockReader(int id, string name, string surname, string email, string phoneNumber, string role)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.role = role;
        }
    }
}
