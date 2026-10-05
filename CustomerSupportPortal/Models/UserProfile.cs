using System;
using System.Collections.Generic;

namespace CustomerSupportPortal.Models;

public partial class UserProfile
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public virtual ICollection<Request> RequestAssignedToNavigations { get; set; } = new List<Request>();

    public virtual ICollection<RequestComment> RequestComments { get; set; } = new List<RequestComment>();

    public virtual ICollection<Request> RequestCustomers { get; set; } = new List<Request>();

    public virtual UserRole Role { get; set; } = null!;
}
