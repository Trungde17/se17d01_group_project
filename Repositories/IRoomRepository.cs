using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRoomRepository
    {
        List<Room> GetRooms();
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
    }
}
