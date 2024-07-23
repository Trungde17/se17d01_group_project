using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRoomService
    {
        List<Room> GetRooms();
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
    }
}
