using Azure.Core;
using CustomerSupportPortal.Models;
using Microsoft.EntityFrameworkCore;
namespace CustomerSupportPortal.Repositories
{
    public class SupportRequestRepository : ISupportRequestRepository
    {
        private readonly CustomerSupportContext _context;
        public SupportRequestRepository(CustomerSupportContext context)
        {
            _context = context;
        }
        public RequestResult GetAllRequests(int start, int length,string search)
        {
            int pageNumber = (start / length) + 1;
            var query = _context.Requests.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                    x.Subject.Contains(search) ||
                    x.Description.Contains(search) ||
                    x.Customer.UserName.Contains(search) ||////navigation of Request entity class
                    x.AssignedToNavigation.UserName.Contains(search) ||//navigation of Request entity class
                    x.Priority.PriorityType.Contains(search) ||
                    x.Status.StatusType.Contains(search)
                );
            }
            var requestDetails = query
               .Skip(start)
               .Take(length)
               .Select(r => new RequestDetails
               {
                   Subject = r.Subject,
                   CustomerName = r.Customer.UserName,
                   AssignedTo = r.AssignedToNavigation.UserName,
                   Priority = r.Priority.PriorityType,
                   Description = r.Description,
                   Status = r.Status.StatusType
               })
               .ToList();
            var filteredRecords = query.Count();
            return new RequestResult
            {
                TotalRecords = filteredRecords,
                Data = requestDetails
            };
        }
    }
}
