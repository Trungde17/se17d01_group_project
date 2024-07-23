using BusinessObjects.Models;
using DataAccessaLayers;
using System.Collections.Generic;

namespace Services
{
    public class BookingService : IBookingService
    {
        public List<Booking> GetBookings() => BookingDAO.Instance.GetBookings();

        public void AddBooking(Booking booking) => BookingDAO.Instance.Add(booking);

        public void UpdateBooking(Booking booking) => BookingDAO.Instance.Update(booking);

        public Booking GetBookingById(int bookingId) => BookingDAO.Instance.GetBookingById(bookingId);

        public void DeleteBooking(int bookingId) => BookingDAO.Instance.Delete(bookingId);
    }
}
