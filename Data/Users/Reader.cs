
namespace Data.Users
{
    public class Reader : User
    {
        public List<Guid> borrowedBooks { get; set; }
        public Reader(string name, string surname, string email, string phoneNumber, UserRole role, double debt) 
            : base(name, surname, email, phoneNumber, role)
        {
            this.borrowedBooks = new List<Guid>();
        }
        public void BorrowBook(Guid bookId)
        {
            borrowedBooks.Add(bookId);
        }

        public void ReturnBook(Guid bookId)
        {
            borrowedBooks.Remove(bookId);
        }

    }
}
