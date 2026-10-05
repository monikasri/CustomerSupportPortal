using System;
using System.Collections.Generic;

namespace CustomerSupportPortal.Models;

public partial class Status
{
    public int StatusId { get; set; }

    public string? StatusType { get; set; }

    public virtual ICollection<Request> Requests { get; set; } = new List<Request>();
}
