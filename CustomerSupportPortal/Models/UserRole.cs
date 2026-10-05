using System;
using System.Collections.Generic;

namespace CustomerSupportPortal.Models;

public partial class UserRole
{
    public int? RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<UserProfile> UserProfiles { get; set; } = new List<UserProfile>();
}
