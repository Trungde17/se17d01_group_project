using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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

        // Add, Update, Delete methods here...
    }
}
