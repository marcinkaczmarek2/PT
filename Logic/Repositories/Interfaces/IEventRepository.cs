using Data.API.Models;

namespace Logic.Repositories.Interfaces
{
    public interface IEventRepository
    {
        void AddEvent(IEventD eventBase);
        List<IEventD> GetAllEvents();
    }
}
