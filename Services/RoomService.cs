using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository iRoomRepository;
        public RoomService()
        {
            iRoomRepository = new RoomRepository();
        }
        public void AddRoom(Room room)
        {
            iRoomRepository.AddRoom(room);
        }

        public void DeleteRoom(Room room)
        {
            iRoomRepository.DeleteRoom(room);
        }

        public List<Room> GetRooms()
        {
           return  iRoomRepository.GetRooms();
        }

        public void UpdateRoom(Room room)
        {
            iRoomRepository.UpdateRoom(room);
        }
    }
}
