using System.Collections.Generic;
using Models;
namespace Repositories
{
    public interface IUserRepository
    {
        void CreateUser(User user);
        List<User> GetUsers();
        void UpdateUserRole(int userId, string newRole);
    }
}