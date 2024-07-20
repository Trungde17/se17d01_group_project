using BusinessObjects.Models;
using DataAccessaLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetUsers()=>UserDAO.Instance.GetUsers();
    }
}
