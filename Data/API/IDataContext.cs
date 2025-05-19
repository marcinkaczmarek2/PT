
namespace Data.API
{
    internal interface IDataContext
    {
        // ----------- Book -----------
        public IQueryable<IBook> Book { get; }
        Task AddBookAsync(IBook bookEntry);
        Task RemoveBookAsync(int id);

        // ----------- Reader -----------
        public IQueryable<IUser> Reader { get; }
        Task AddReaderAsync(IUser readerEntry);
        Task RemoveReaderAsync(int id);

        // ----------- Event -----------
        public IQueryable<IEvent> Event { get; }
        Task AddEventAsync(IEvent eventEntry);
        Task RemoveEventAsync(int id);

        // ----------- State -----------
        public IQueryable<IState> State { get; }
        Task AddStateAsync(IState stateEntry);
        Task RemoveStateAsync(int id);
    }
}
