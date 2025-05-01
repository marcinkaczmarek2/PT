using Data.API.Models;
using Logic.Repositories;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal sealed class EventService : IEventService
    {
        private readonly EventRepository eventRepository;

        internal EventService(EventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public bool AddEvent(IEvent eventBase)
        {
            if (eventBase == null)
            {
                throw new InvalidOperationException("Error, event cannot be null.");
            }

            eventRepository.AddEvent(eventBase);
            return true;
        }

        public List<IEvent> GetAllEvents()
        {
            var receivedList = eventRepository.GetAllEvents();
            if (receivedList.Count == 0)
            {
                throw new InvalidOperationException("Error, no events found.");
            }
            return receivedList;
        }
    }
}
