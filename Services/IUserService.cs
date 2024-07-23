using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User GetUserById(int userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int userId);
    }
}
