using Azure.Core;
using CustomerSupportPortal.Models;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Request = CustomerSupportPortal.Models.Request;
namespace CustomerSupportPortal.Repositories
{
    public class SupportRequestRepository : ISupportRequestRepository
    {
        private readonly CustomerSupportContext _context;
        public SupportRequestRepository(CustomerSupportContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All Requests Created by user to display on Screens for Agent/Admin/Customer
        /// </summary>
        /// <param name="start"></param>
        /// <param name="length"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public RequestResult GetAllRequests(int start, int length, string search)
        {
            int pageNumber = (start / length) + 1;
            var query = _context.Requests.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(x =>
                   (x.Subject != null && x.Subject.Contains(search)) ||
                   (x.Description != null && x.Description.Contains(search)) ||
                   (x.Customer != null && x.Customer.UserName != null && x.Customer.UserName.Contains(search)) ||////navigation of Request entity class
                   (x.AssignedToNavigation != null && x.AssignedToNavigation.UserName != null && x.AssignedToNavigation.UserName.Contains(search)) ||//navigation of Request entity class
                   (x.Priority != null && x.Priority.PriorityType != null && x.Priority.PriorityType.Contains(search)) ||
                   (x.Status != null && x.Status.StatusType != null && x.Status.StatusType.Contains(search))
                );
            }
            var requestDetails = query
               .Skip(start)
               .Take(length)
               .Select(r => new RequestDetails
               {
                   Subject = r.Subject,
                   CustomerName = r.Customer != null ? r.Customer.UserName : null,
                   AssignedTo = r.AssignedToNavigation != null ? r.AssignedToNavigation.UserName : null,
                   AssignedToId=r.AssignedToNavigation.UserId != null ? r.AssignedToNavigation.UserId : null,
                   Priority = r.Priority != null ? r.Priority.PriorityType : null,
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
        /// <summary>
        /// GetPriorities : Get All the Priorities like High,Low,Medium and Critical
        /// </summary>
        /// <returns></returns>
        public List<Priority> GetPriorities()
        {
            var priorities = _context.Priorities
                .Select(x => new Priority
                {
                    PriorityId = x.PriorityId,
                    PriorityType = x.PriorityType
                }).ToList();

            return priorities;
        }
        /// <summary>
        /// CreateRequest : It will Create New Request By saving Data for logged in Customer
        /// </summary>
        /// <param name="requestDetails"></param>
        /// <param name="email"></param>
        public void CreateRequest(RequestDetails requestDetails, string? email)
        {
         int? customerId = _context.UserProfiles.SingleOrDefault(x=>x.Email == email)?.UserId;
            var request = new Request
            {
                Subject = requestDetails.Subject,
                PriorityId = requestDetails.PriorityId,
                Description = requestDetails.Description,
                CustomerId = customerId,
                StatusId=1
            };
            _context.Requests.Add(request);
            _context.SaveChanges();
        }
    }
}
