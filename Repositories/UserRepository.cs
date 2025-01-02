using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Services;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseService _databaseService;

        public UserRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public void CreateUser(string name, string email, string passwordHash, string role)
        {
            var query = "INSERT INTO Users (Name, Email, PasswordHash, Role) VALUES (@Name, @Email, @PasswordHash, @Role)";
            _databaseService.ExecuteNonQuery(query, command =>
            {
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.Parameters.AddWithValue("@Role", role);
            });
        }

        public List<string> GetUsers()
        {
            var query = "SELECT Name FROM Users";
            return _databaseService.ExecuteReader(query, reader => reader.GetString(0));
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