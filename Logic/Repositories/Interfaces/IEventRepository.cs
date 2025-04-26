using Data.Events;

namespace Logic.Repositories.Interfaces
{
    public interface IEventRepository
    {
        void AddEvent(EventBase eventBase);
        List<EventBase> GetAllEvents();
    }
}
