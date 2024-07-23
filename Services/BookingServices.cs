using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingServices : IBookingService
    {
        private readonly IBookingRepository _repository;
        public BookingServices() 
        {
            _repository = new BookingRepository();
        }

        public void Add(Booking booking)=>_repository.Add(booking);

        public List<Booking> GetBookings() =>_repository.GetBookings();
    }
}
