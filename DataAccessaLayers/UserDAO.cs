using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessaLayers
{
    public class UserDAO
    {
        private static UserDAO instance;
        private static readonly object locker = new object();

        public static UserDAO Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        private UserDAO() { }

        public List<User> GetUsers()
        {
            List<User> users;
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    users = applicationDbContext.Users.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return users;
        }

        public void Update(User user)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(User user)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    var us = applicationDbContext.Users.SingleOrDefault(u => u.UserId == user.UserId);
                    applicationDbContext.Remove(us);
                    applicationDbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Add(User user)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Users.Add(user);
                    applicationDbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
