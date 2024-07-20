using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessaLayers
{
    public class BookingDAO
    {
        private static BookingDAO instance;
        private static readonly object locker = new object();

        public static BookingDAO Instance
        {
            get
            {
                lock (locker)
                {
                    if (instance == null)
                    {
                        instance = new BookingDAO();
                    }
                    return instance;
                }
            }
        }

        private BookingDAO() { }

        public List<Booking> GetBookings()
        {
            List<Booking> bookings;
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    bookings = applicationDbContext.Bookings.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return bookings;
        }

        public void Update(Booking booking)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Entry<Booking>(booking).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    applicationDbContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Delete(Booking booking)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    var bo = applicationDbContext.Bookings.SingleOrDefault( b => b.BookingId == booking.BookingId);
                    applicationDbContext.Remove(bo);
                    applicationDbContext.SaveChanges();

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Add(Booking booking)
        {
            try
            {
                using (MyHomestayContext applicationDbContext = new MyHomestayContext())
                {
                    applicationDbContext.Bookings.Add(booking);
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
