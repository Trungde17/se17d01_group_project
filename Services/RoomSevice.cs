using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomSevice : IRoomService
    {
        private readonly IRoomRepository roomRepository;
        public RoomSevice()
        {
            roomRepository = new RoomRepository();
        }
        public List<Room> GetRooms()=>roomRepository.GetRooms();
    }
}
