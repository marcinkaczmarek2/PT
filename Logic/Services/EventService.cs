using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using Data.Events;
using Logic.Repositories;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    public class EventService : IEventService
    {
        private readonly EventRepository eventRepository;

        public EventService(EventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public bool AddEvent(EventBase eventBase)
        {
            if (eventBase == null)
            {
                throw new Exception("Error, event cannot be null.");
            }

            eventRepository.AddEvent(eventBase);
            return true;
        }

        public List<EventBase> GetAllEvents()
        {
            var receivedList = eventRepository.GetAllEvents();
            if (receivedList.Count == 0)
            {
                throw new Exception("Error, no events found.");
            }
            return receivedList;
        }
    }
}
