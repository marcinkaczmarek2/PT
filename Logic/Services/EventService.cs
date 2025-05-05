using System;
using System.Collections.Generic;
using Logic.Models;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal sealed class EventService : IEventService
    {
        private readonly List<IEventL> events = new();

        public bool AddEvent(IEventL e)
        {
            if (e == null)
                throw new ArgumentNullException(nameof(e));

            events.Add(e);
            return true;
        }

        public List<IEventL> GetAllEvents()
        {
            return new List<IEventL>(events); 
        }
    }
}
