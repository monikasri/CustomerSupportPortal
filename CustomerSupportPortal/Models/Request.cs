using System;
using System.Collections.Generic;

namespace CustomerSupportPortal.Models;

public partial class Request
{
    public int RequestId { get; set; }

    public int StatusId { get; set; }

    public string? Subject { get; set; }

    public int? AssignedTo { get; set; }

    public int? CustomerId { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public int? PriorityId { get; set; }

    public virtual UserProfile? AssignedToNavigation { get; set; }

    public virtual UserProfile? Customer { get; set; }

    public virtual Priority? Priority { get; set; }

    public virtual ICollection<RequestComment> RequestComments { get; set; } = new List<RequestComment>();

    public virtual Status Status { get; set; } = null!;
}
