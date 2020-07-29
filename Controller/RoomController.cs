using Bolnica.Model;
using Bolnica.Service;
using System.Collections.Generic;

namespace Bolnica.Controller
{
    public class RoomController : IController<Room, long>
    {
        private readonly RoomService service;

        public RoomController(RoomService service)
        {
            this.service = service;
        }

        public IEnumerable<Room> GetAll() => service.GetAll();

        public Room GetOne(long id) => service.GetOne(id);

        public Room Create(Room room) => service.Create(room);

        public void Update(Room room) => service.Update(room);

        public void Delete(Room room) => service.Delete(room);
        public void GetValidRooms() => service.GetValidRooms();
    }
}
