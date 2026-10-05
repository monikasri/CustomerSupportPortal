using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Repositories
{
    public interface ISupportRequestRepository
    {
        RequestResult GetAllRequests(int start, int length, string search);
    }
}
