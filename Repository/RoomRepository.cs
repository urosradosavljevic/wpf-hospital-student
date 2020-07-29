using Bolnica.Model;
using Bolnica.Repository.Abstract;
using Bolnica.Repository.CSV;
using Bolnica.Repository.CSV.Stream;
using Bolnica.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolnica.Repository
{
    public class RoomRepository : CSVRepository<Room,long>,
        IRoomRepository,
        IEagerCSVRepository<Room,long>
    {

        private const string ENTITY_NAME = "Room";

        public RoomRepository(ICSVStream<Room> stream, ISequencer<long> sequencer): base(ENTITY_NAME, stream,sequencer){}
        public new IEnumerable<Room> Find(Func<Room, bool> predicate) => GetAllEager().Where(predicate);

        public Room GetEager(long id) => GetOne(id);
        public IEnumerable<Room> GetAllEager() => GetAll();
        public IEnumerable<Room> GetValidRooms() => Find(room => room.RoomNumber != 0);
    }
}
