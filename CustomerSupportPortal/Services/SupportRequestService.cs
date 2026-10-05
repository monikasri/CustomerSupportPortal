using CustomerSupportPortal.Models;
using CustomerSupportPortal.Repositories;
namespace CustomerSupportPortal.Services
{
    public class SupportRequestService : ISupportRequestService
    {
        private readonly ISupportRequestRepository _repository;
        public SupportRequestService(ISupportRequestRepository repository)
        {
            _repository = repository;
        }
        public RequestResult GetAllRequests(int start, int length,string search)
        {
          return _repository.GetAllRequests(start, length,search);
        }
    }
}
