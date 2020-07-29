using Bolnica.Exception;
using Bolnica.Repository.Abstract;
using Bolnica.Repository.CSV.Stream;
using Bolnica.Repository.Sequencer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bolnica.Repository.CSV
{
    public class CSVRepository<E,ID>: IRepository<E, ID>
        where E : IIdentifiable<ID>
        where ID : IComparable
    {
        private const string NOT_FOUND_ERROR = "{0} with {1}:{2} can not be found!";

        protected string _entityName;
        protected readonly ICSVStream<E> _stream;
        protected readonly ISequencer<ID> _sequencer;

        public CSVRepository(string entityName, ICSVStream<E> stream, ISequencer<ID> sequencer)
        {
            _entityName = entityName;
            _stream = stream;
            _sequencer = sequencer;
            InitializeId();
        }

        public E Create(E entity)
        {
            entity.SetId(_sequencer.GenerateId());
            _stream.AppendLineToFile(entity);
            return entity;
        }


        public void Delete(E entity)
        {
            var entities = _stream.ReadAll().ToList();
            var entityToRemove = entities.SingleOrDefault(ent => ent.GetId().CompareTo(entity.GetId()) == 0);
            if (entityToRemove != null)
            {
                entities.Remove(entityToRemove);
                _stream.SaveAll(entities);
            }
            else
            {
                ThrowEntityNotFoundException("id", entity.GetId());
            }
        }

        public IEnumerable<E> Find(Func<E, bool> predicate)
            => _stream
            .ReadAll()
            .Where(predicate);


        public E GetOne(ID id)
        {
            try
            {
                return _stream
                    .ReadAll()
                    .SingleOrDefault(entity => entity.GetId().CompareTo(id) == 0);
            }
            catch (ArgumentException)
            {
                throw new EntityNotFoundException(string.Format(NOT_FOUND_ERROR, _entityName, "id", id));
            }
        }

        public IEnumerable<E> GetAll() => _stream.ReadAll();

        public void Update(E entity)
        {
            try
            {
                var entities = _stream.ReadAll().ToList();

                entities[entities.FindIndex(ent => ent.GetId().CompareTo(entity.GetId()) == 0)] = entity;

                _stream.SaveAll(entities);
            }
            catch (ArgumentException)
            {
                ThrowEntityNotFoundException("id", entity.GetId());
            }
        }

        protected void InitializeId() => _sequencer.Initialize(GetMaxId(_stream.ReadAll()));

        private ID GetMaxId(IEnumerable<E> entities)
           => entities.Count() == 0 ? default : entities.Max(entity => entity.GetId());

        private void ThrowEntityNotFoundException(string key, object value)
          => throw new EntityNotFoundException(string.Format(NOT_FOUND_ERROR, _entityName, key, value));
    }
}
