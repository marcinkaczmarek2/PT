using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using Data.API.Models;
using Logic.Repositories.Interfaces;
using Logic.Services.Interfaces;

namespace Logic.Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEventService eventService;
        private readonly IEventFactory eventFactory;
        private readonly IUserFactory userFactory;

        internal UserService(IUserRepository userRepository, IEventService eventService, IEventFactory eventFactory, IUserFactory userFactory)
        {
            this.userRepository = userRepository;
            this.eventService = eventService;
            this.eventFactory = eventFactory;
            this.userFactory = userFactory;
        }

        public bool AddUser(IUser user)
        {
            if (userRepository.GetUser(user.id) != null)
            {
                throw new InvalidOperationException("Error, cannot add another user with the same id.");
            }

            userRepository.AddUser(user);
            eventService.AddEvent(eventFactory.CreateUserAddedEvent(user.id, user.email));
            return true;
        }

        public bool RemoveUser(Guid id)
        {
            var existingUser = userRepository.GetUser(id);
            if (existingUser == null)
            {
                return false;
            }

            eventService.AddEvent(eventFactory.CreateUserRemovedEvent(existingUser.id, existingUser.email));
            return userRepository.RemoveUser(id);
        }

        public IUser GetUser(Guid id)
        {
            var receivedUser = userRepository.GetUser(id);
            if (receivedUser == null)
            {
                throw new InvalidOperationException("Error, no user with such id.");
            }
            return receivedUser;
        }

        public List<IUser> GetAllUsers()
        {
            var receivedList = userRepository.GetAllUsers();
            if (receivedList.Count == 0)
            {
                throw new InvalidOperationException("Error, no users found.");
            }
            return receivedList;
        }

        public IUser CreateReader(string name, string surname, string email, string phoneNumber)
        {
            var existingUser = userRepository.GetAllUsers().FirstOrDefault(u => u.email == email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Error, User already exists with this email.");
            }

            if (!IsValidEmail(email))
            {
                throw new InvalidOperationException("Error, email must have '@' and '.' signs in it.");
            }

            if (!IsValidPhoneNumber(phoneNumber))
            {
                throw new InvalidOperationException("Error, phone number can consist of only numbers.");
            }

            return userFactory.CreateReader(name, surname, email, phoneNumber);
        }

        public bool RegisterReader(string name, string surname, string email, string phoneNumber)
        {
            try
            {
                var newReader = CreateReader(name, surname, email, phoneNumber);
                return AddUser(newReader);
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine($"{e.Message}");
                return false;
            }
        }

        private bool IsValidEmail(string email)
        {
            var emailRegex = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailRegex);
        }

        private bool IsValidPhoneNumber(string phoneNumber)
        {
            var phoneRegex = @"^\+?\d+$";
            return Regex.IsMatch(phoneNumber, phoneRegex);
        }
    }
}
