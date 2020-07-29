using Bolnica.Model;
using Bolnica.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Bolnica.Service
{
    public class FeedbackService : IService<Feedback, long>
    {

        private readonly IRepository<Feedback,long> _repository;

        public FeedbackService(FeedbackRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Feedback> GetAll() => _repository.GetAll();

        public Feedback Create(Feedback feedback) => _repository.Create(feedback);

        public void Delete(Feedback feedback) => _repository.Delete(feedback);

        public Feedback GetOne(long id) => _repository.GetOne(id);

        public void Update(Feedback feedback) => _repository.Update(feedback);

        IEnumerable<Feedback> IService<Feedback, long>.GetAll() => _repository.GetAll();

        /*

        public Doctor[] ViewAvailableDoctors(DateTime start, DateTime end)
        {
            // TODO: implement+
            return null;
        }

        public int ViewAvailableDates(int feedbackId)
        {
            // TODO: implement+
            return 0;
        }

        public DateTime[] ViewAvaliableDates(int feedbackID)
        {
            // TODO: implement +
            return null;
        }

        public DateTime[] ViewAvaliableTerms(int feedbackID, DateTime date)
        {
            // TODO: implement +
            return null;
        }
        */
    }
}
