using Data.Events;

namespace Logic.Services.Interfaces
{
    public interface IEventService
    {
        bool AddEvent(EventBase eventBase);
        List<EventBase> GetAllEvents();
    }

}
