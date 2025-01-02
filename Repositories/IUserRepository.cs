using System.Collections.Generic;

namespace Repositories
{
    public interface IUserRepository
    {
        void CreateUser(string name, string email, string passwordHash, string role);
        List<string> GetUsers();
        void UpdateUserRole(int userId, string newRole);
    }
}