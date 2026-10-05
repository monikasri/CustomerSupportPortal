using CustomerSupportPortal.Models;
namespace CustomerSupportPortal.Services
{
    public interface ISupportRequestService
    {
        RequestResult GetAllRequests(int start, int length, string search);
    }
}
