using BusinessObjects.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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
                    bookings = applicationDbContext.Bookings.Include(b=>b.BookingRooms).ToList();
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
                    // Add Booking first to get the BookingID generated
                    applicationDbContext.Bookings.Add(booking);
                    applicationDbContext.SaveChanges();

                    // Now add BookingRooms with the generated BookingID
                    
                }
            }
            catch (DbUpdateException dbEx)
            {
                var sqlEx = dbEx.GetBaseException() as SqlException;
                if (sqlEx != null)
                {
                    Console.WriteLine($"SQL Error: {sqlEx.Message}");
                    foreach (SqlError error in sqlEx.Errors)
                    {
                        Console.WriteLine($"Error Code: {error.Number}, Message: {error.Message}");
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
                throw;
            }
        }
    }
}
