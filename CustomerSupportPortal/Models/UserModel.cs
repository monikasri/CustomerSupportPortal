using System;
using System.Collections.Generic;

namespace CustomerSupportPortal.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string? UserName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public int? RoleId { get; set; }
        public string? RoleName { get; set; }

    }
}
