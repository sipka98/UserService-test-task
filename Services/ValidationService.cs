using System;
using System.Text.RegularExpressions;

namespace Services
{
    public class ValidationService
    {
        public void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name is required");
            }
        }

        public void ValidateEmail(string email)
        {
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
            {
                throw new ArgumentException("Invalid email format");
            }
        }

        public void ValidatePassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6)
            {
                throw new ArgumentException("Password must be at least 6 characters long");
            }
        }

        public void ValidateRole(string role)
        {
            if (role != "Admin" && role != "User")
            {
                throw new ArgumentException("Invalid role");
            }
        }
    }
}