using Microsoft.AspNetCore.Identity;

namespace UserSecurityManagerProjectApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime Birthday { get; set; }
    }
}
