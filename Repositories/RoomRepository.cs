using BusinessObjects.Models;
using DataAccessaLayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RoomRepository : IRoomRepository
    {
        public void AddRoom(Room room)=>RoomDAO.Instance.Add(room);
       

        public void DeleteRoom(Room room) => RoomDAO.Instance.Delete(room);
       

        public List<Room> GetRooms() => RoomDAO.Instance.GetRooms();

        public void UpdateRoom(Room room)=> RoomDAO.Instance.Update(room);
        
    }
}