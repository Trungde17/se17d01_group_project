using BusinessObjects.Models;
using DataAccessaLayers;
using System.Collections.Generic;

namespace Services
{
    public class UserService : IUserService
    {
        public List<User> GetUsers() => UserDAO.Instance.GetUsers();

        public void AddUser(User user) => UserDAO.Instance.Add(user);

        public void UpdateUser(User user) => UserDAO.Instance.Update(user);

        public User GetUserById(int userId) => UserDAO.Instance.GetUserById(userId);

        public void DeleteUser(int userId) => UserDAO.Instance.Delete(userId);
    }
}
