using Bolnica.Model;
using System.Collections.Generic;

namespace Bolnica.Repository.Abstract
{
    public interface IRoomRepository : IRepository<Room, long>
    {
        IEnumerable<Room> GetValidRooms();
    }
}
