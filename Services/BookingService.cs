using BusinessObjects.Models;
using DataAccessaLayers;
using System.Collections.Generic;

namespace Services
{
    public class BookingService : IBookingService
    {
        public List<Booking> GetBookings() => BookingDAO.Instance.GetBookings();
    }
}
