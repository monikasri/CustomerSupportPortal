using System;
using System.Collections.Generic;

namespace CustomerSupportPortal.Models;

public partial class Priority
{
    public int PriorityId { get; set; }

    public string? PriorityType { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
