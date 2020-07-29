using Bolnica.Model;
using Bolnica.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bolnica.Controller
{
    public class FeedbackController:IController<Feedback, long>
    {
        private readonly FeedbackService _service;

        public FeedbackController(FeedbackService service)
        {
            _service = service;
        }

        public IEnumerable<Feedback> GetAll() => _service.GetAll();

        public Feedback GetOne(long id) => _service.GetOne(id);

        public Feedback Create(Feedback feedback) => _service.Create(feedback);

        public void Update(Feedback feedback) => _service.Update(feedback);

        public void Delete(Feedback feedback) => _service.Delete(feedback);
    }
}
