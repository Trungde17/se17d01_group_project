using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessaLayers
{
    public class BookingRoomDAO
    {
        private static BookingRoomDAO instance;
        private static readonly object locker = new object();

        public static BookingRoomDAO Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new BookingRoomDAO();
                    }
                    return instance;
                }
            }
        }

        private BookingRoomDAO() { }

        public List<BookingRoom> GetBookingRooms()
        {
            List<BookingRoom> bookingRooms;
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    bookingRooms = applicationDbContext.BookingRooms.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return bookingRooms;
        }

        public void Update(BookingRoom bookingRoom)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Entry<BookingRoom>(bookingRoom).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(BookingRoom bookingRoom)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    var br = applicationDbContext.BookingRooms.SingleOrDefault(br => br.BookingRoomId  == br.BookingRoomId);
                    applicationDbContext.Remove(br);
                    applicationDbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Add(BookingRoom bookingRoom)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.BookingRooms.Add(bookingRoom);
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
