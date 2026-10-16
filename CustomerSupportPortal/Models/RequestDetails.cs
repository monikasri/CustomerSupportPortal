namespace CustomerSupportPortal.Models
{
    public class RequestDetails
    {
        public string? Subject { get; set; }
        public string? CustomerName { get; set; }
        public string? AssignedTo { get; set; }
        public int? AssignedToId { get; set; }
        public string? Priority { get; set; }
        public int? PriorityId { get; set; }
        public int? CusomerId { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public int? RequestId { get; set; }
    }
}
