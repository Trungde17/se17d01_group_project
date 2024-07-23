using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    return applicationDbContext.Users.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting users: " + e.Message);
                return new List<User>();
            }
        }

        public User GetUserById(int userId)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    return applicationDbContext.Users.SingleOrDefault(u => u.UserId == userId);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error getting user by ID: " + e.Message);
                return null;
            }
        }

        public void Update(User user)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error updating user: " + e.Message);
            }
        }

        public void Delete(int userId)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    var user = applicationDbContext.Users.SingleOrDefault(u => u.UserId == userId);
                    if (user != null)
                    {
                        applicationDbContext.Users.Remove(user);
                        applicationDbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error deleting user: " + e.Message);
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
                Console.WriteLine("Error adding user: " + e.Message);
            }
        }
    }
}
