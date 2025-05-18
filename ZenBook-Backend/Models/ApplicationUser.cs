using Microsoft.AspNetCore.Identity;

namespace ZenBook_Backend.Models
{
    public class ApplicationUser : IdentityUser
    {
        // For multi-tenant: store which tenant this user belongs to
        public string TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}