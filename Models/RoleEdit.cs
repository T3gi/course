using Microsoft.AspNetCore.Identity;

namespace Phoenix.Models
{
    public class RoleEdit
    {
        public required IdentityRole Role { get; set; }
        public required IEnumerable<IdentityUser> Members { get; set; }
        public required IEnumerable<IdentityUser> NonMembers { get; set; }
    }
}
