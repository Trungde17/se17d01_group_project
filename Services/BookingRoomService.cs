using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class BookingRoomService : IBookingRoomService
    {
        private readonly IBookingRoomRepository _repository;
        public BookingRoomService()
        {
                _repository = new BookingRoomRepository();
        }
        public void Add(BookingRoom bookingRoom)=>_repository.Add(bookingRoom); 
    }
}
