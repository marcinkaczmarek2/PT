using Data.API.Models;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Logic.Services
{
    internal sealed class EventService : IEventService
    {
        private readonly IEventRepository eventRepository;

        internal EventService(IEventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public bool AddEvent(IEvent eventBase)
        {
            if (eventBase == null)
                throw new InvalidOperationException("Error, event cannot be null.");

            eventRepository.AddEvent(eventBase);
            return true;
        }

        public List<IEvent> GetAllEvents()
        {
            var receivedList = eventRepository.GetAllEvents();
            if (receivedList.Count == 0)
                throw new InvalidOperationException("Error, no events found.");

            return receivedList;
        }
    }
}
