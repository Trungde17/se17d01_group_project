using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        User GetUserById(int userId);
        void DeleteUser(int userId);
    }
}
