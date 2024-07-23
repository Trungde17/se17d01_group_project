using BusinessObjects.Models;
using DataAccessaLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class BookingRepository : IBookingRepository
    {
        public void Add(Booking booking)=>BookingDAO.Instance.Add(booking);
        public List<Booking> GetBookings()=>BookingDAO.Instance.GetBookings();
    }
}
