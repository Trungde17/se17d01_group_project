using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessaLayers
{
    public class RoomDAO
    {
        private static RoomDAO instance;
        private static readonly object locker = new object();

        public static RoomDAO Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new RoomDAO();
                    }
                    return instance;
                }
            }
        }

        private RoomDAO() { }

        public List<Room> GetRooms()
        {
            List<Room> rooms;
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    rooms = applicationDbContext.Rooms.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return rooms;
        }

        public void Update(Room room)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Entry<Room>(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Room room)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    var cus = applicationDbContext.Rooms.SingleOrDefault(r => r.RoomId == r.RoomId);
                    applicationDbContext.Remove(cus);
                    applicationDbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Add(Room room)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Rooms.Add(room);
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
