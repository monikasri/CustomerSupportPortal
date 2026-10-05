using System;
using System.Collections.Generic;

namespace CustomerSupportPortal.Models;

public partial class RequestComment
{
    public int CommentId { get; set; }

    public int? RequestId { get; set; }

    public int? UserId { get; set; }

    public string? Comments { get; set; }

    public DateTime? CreatedDate { get; set; }

    public virtual Request? Request { get; set; }

    public virtual UserProfile? User { get; set; }
}
