using System;
using System.Collections.Generic;
using Logic.Models;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal sealed class LibraryService : ILibraryService
    {
        private readonly ILibraryRepository libraryRepository;
        private readonly IEventService eventService;
        private readonly IEventFactory eventFactory;

        internal LibraryService(
            ILibraryRepository libraryRepository,
            IEventService eventService,
            IEventFactory eventFactory)
        {
            this.libraryRepository = libraryRepository;
            this.eventService = eventService;
            this.eventFactory = eventFactory;
        }

        public bool AddContent(IBorrowableL content)
        {
            if (libraryRepository.GetContent(content.Id) != null)
            {
                throw new InvalidOperationException("Error, cannot add another item with the same id.");
            }

            libraryRepository.AddContent(content);
            eventService.AddEvent(eventFactory.CreateItemAddedEvent(content.Id, content.Title));
            return true;
        }

        public bool RemoveContent(Guid id)
        {
            var existingContent = libraryRepository.GetContent(id);
            if (existingContent == null)
            {
                return false;
            }

            eventService.AddEvent(eventFactory.CreateItemRemovedEvent(existingContent.Id, existingContent.Title));
            return libraryRepository.RemoveContent(id);
        }

        public IBorrowableL? GetContent(Guid id)
        {
            var content = libraryRepository.GetContent(id);
            if (content == null)
            {
                throw new InvalidOperationException("Error, no item with such id.");
            }
            return content;
        }

        public List<IBorrowableL> GetAllContent()
        {
            var items = libraryRepository.GetAllContent();
            if (items.Count == 0)
            {
                throw new InvalidOperationException("Error, no items in stock.");
            }
            return items;
        }
    }
}
