using Bolnica.Model;
using Bolnica.Repository;
using System.Collections.Generic;

namespace Bolnica.Service
{
    public class RoomService : IService<Room, long>
    {
        private readonly RoomRepository _repository;

        public RoomService(RoomRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Room> GetAll() => _repository.GetAll();
        public IEnumerable<Room> GetValidRooms() => _repository.GetValidRooms();

        public Room GetOne(long id) => _repository.GetOne(id);

        public Room Create(Room room) => _repository.Create(room);

        public void Update(Room room) => _repository.Update(room);

        public void Delete(Room room) => _repository.Delete(room);

    }
}
