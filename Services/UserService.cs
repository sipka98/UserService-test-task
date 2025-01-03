using System;
using System.Collections.Generic;
using Services;
using Repositories;
using Models;
namespace Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidationService  _validationService;

        public UserService(IUserRepository userRepository, IValidationService  validationService)
        {
            _userRepository = userRepository;
            _validationService = validationService;
        }

        public void CreateUser(string name, string email, string password, string role)
        {
            _validationService.ValidateUser(name, email, password, role);

            var user = new User
                {
                    Name = name,
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
                    Role = role
                };

            _userRepository.CreateUser(user);
        }

        public List<User> GetUsers()
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