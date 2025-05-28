using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Phoenix.Areas.Identity.Data;

namespace Phoenix.CustomTagHelpers
{
    [HtmlTargetElement("td", Attributes = "i-role")]
    public class RoleUsersTH : TagHelper
    {
        private UserManager<IdentityUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        public RoleUsersTH(RoleManager<IdentityRole> rolemgr, UserManager<IdentityUser> usermgr)
        {
            userManager = usermgr;
            roleManager = rolemgr;
        }

        [HtmlAttributeName("i-role")]
        public string Role { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            List<string> names = new List<string>();
            var role = await roleManager.Roles
                .FirstOrDefaultAsync(m => m.Id == Role);
            if (role != null)
            {
                foreach (var user in userManager.Users)
                {
                    if (user != null && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }
            output.Content.SetContent(names.Count == 0 ? "No Users" : string.Join(", ", names));
        }
    }
}