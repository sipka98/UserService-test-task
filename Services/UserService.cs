using System;
using System.Collections.Generic;
using Services;
using Repositories;

namespace Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ValidationService _validationService;

        public UserService(IUserRepository userRepository, ValidationService validationService)
        {
            _userRepository = userRepository;
            _validationService = validationService;
        }

        public void CreateUser(string name, string email, string password, string role)
        {
            _validationService.ValidateName(name);
            _validationService.ValidateEmail(email);
            _validationService.ValidatePassword(password);
            _validationService.ValidateRole(role);

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            _userRepository.CreateUser(name, email, passwordHash, role);
        }

        public List<string> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public void UpdateUserRole(int userId, string newRole)
        {
            _validationService.ValidateRole(newRole);
            _userRepository.UpdateUserRole(userId, newRole);
        }
    }
}