using Data.API.Models;

namespace Logic.Services.Interfaces
{
    public interface IEventService
    {
        bool AddEvent(IEvent eventBase);
        List<IEvent> GetAllEvents();
    }

}
