using Data.Catalog;
using Data.Users;
using Data;
using Logic.Repositories;
using Logic.Services;

[TestClass]
public class BorrowServiceTest
{
    private InMemoryDataContext _context;
    private BorrowService _borrowService;
    private Guid _userId;
    private Guid _itemId;

    [TestInitialize]
    public void Initialize()
    {
        _context = new InMemoryDataContext();

        var userRepository = new UserRepository(_context);
        var libraryRepository = new LibraryRepository(_context);
        var eventService = new EventService(new EventRepository(_context));

        _borrowService = new BorrowService(userRepository, libraryRepository, eventService);

        var reader = new Reader("John", "Doe", "john@example.com", "+123", UserRole.Reader, 0.0);
        var book = new Book("The Great Adventure", "Publisher", true, "Author Name", 123, BookGenre.Adventure);

        _context.AddUser(reader);
        _context.AddItem(book);

        _userId = reader.id; 
        _itemId = book.id;   
    }

    [TestMethod]
    public void BorrowItem_ShouldSetItemAsUnavailable()
    {
        bool result = _borrowService.BorrowItem(_userId, _itemId);

        Assert.IsTrue(result, "BorrowItem should return true.");

        List<Borrowable> items = _context.GetItems();
        bool found = false;
        foreach (Borrowable item in items)
        {
            if (item.id == _itemId)
            {
                found = true;
                Assert.IsFalse(item.availbility, "Item should not be available after borrowing.");
                break;
            }
        }
        Assert.IsTrue(found, "Borrowed item should exist.");
    }
}
