using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Services
{
    public interface ISupportRequestService
    {
        RequestResult GetAllRequests(int start, int length, string search);
        List<Priority> GetPriorities();
        void CreateRequest(RequestDetails requestDetails, string? email);
        void AssignRequest(int requestId, int agentId);
    }
}
