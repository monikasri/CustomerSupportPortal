namespace CustomerSupportPortal.Models
{
    public class RequestResult
    {
        public int TotalRecords { get; set; }

        public List<RequestDetails> Data { get; set; }
    }
}
