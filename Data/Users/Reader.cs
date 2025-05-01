using Data.Enums;

namespace Data.Users
{
    internal class Reader : User
    {
        internal List<Guid> borrowedBooks { get; private set; }
        internal Reader(string name, string surname, string email, string phoneNumber, UserRole role, double debt) 
            : base(name, surname, email, phoneNumber, role)
        {
            this.borrowedBooks = new List<Guid>();
        }
        internal void BorrowBook(Guid bookId)
        {
            borrowedBooks.Add(bookId);
        }

        internal void ReturnBook(Guid bookId)
        {
            borrowedBooks.Remove(bookId);
        }

    }
}
