using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IBookingService
    {
        List<Booking> GetBookings();
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        Booking GetBookingById(int bookingId);
        void DeleteBooking(int bookingId);
    }
}
