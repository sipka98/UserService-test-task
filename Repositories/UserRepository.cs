using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Services;
using Models;
namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseService _databaseService;

        public UserRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void CreateUser(User user)
        {
            var query = "INSERT INTO Users (Name, Email, PasswordHash, Role) VALUES (@Name, @Email, @PasswordHash, @Role)";
            _databaseService.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                command.Parameters.AddWithValue("@Role", user.Role);
            });
        }

        public List<User> GetUsers()
        {
            var query = "SELECT Id, Name, Email, PasswordHash, Role FROM Users";
            return _databaseService.ExecuteReader(query, reader => new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                PasswordHash = reader.GetString(3),
                Role = reader.GetString(4)
            });

        }

        public void UpdateUserRole(int userId, string newRole)
        {
            var query = "UPDATE Users SET Role = @Role WHERE Id = @Id";
            _databaseService.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Role", newRole);
                command.Parameters.AddWithValue("@Id", userId);
            });
        }
    }
}