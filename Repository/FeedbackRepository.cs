using Bolnica.Exception;
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
    public class FeedbackRepository : CSVRepository<Feedback, long>,
        IFeedbackRepository,
        IEagerCSVRepository<Feedback,long>
    {

        private const string ENTITY_NAME = "Feedback";

        public FeedbackRepository(ICSVStream<Feedback> stream, 
            ISequencer<long> sequencer
            ) : base(ENTITY_NAME, stream, sequencer)
        {}

        public new IEnumerable<Feedback> Find(Func<Feedback, bool> predicate) => GetAllEager().Where(predicate);

        public Feedback GetEager(long id) => GetOne(id);

        public IEnumerable<Feedback> GetAllEager() => GetAll();

    }
}
