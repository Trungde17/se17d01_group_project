using BusinessObjects.Models;
using System.Collections.Generic;

namespace Services
{
    public interface IBookingService
    {
        List<Booking> GetBookings();
    }
}
