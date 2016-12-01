using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ADL.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace ADL.Infrastructure
{
    [HtmlTargetElement("ul", Attributes = "identity-role")]
    public class RoleUsersTagHelper : TagHelper
    {
        private UserManager<Person> userManager;
        private RoleManager<IdentityRole> roleManager;
        public RoleUsersTagHelper(UserManager<Person> usermgr,
                                  RoleManager<IdentityRole> rolemgr)
        {
            userManager = usermgr;
            roleManager = rolemgr;
        }
        [HtmlAttributeName("identity-role")]
        public string Role { get; set; }
        public override async Task ProcessAsync(TagHelperContext context,
                TagHelperOutput output)
        {
            List<string> names = new List<string>();
            IdentityRole role = await roleManager.FindByIdAsync(Role);
            if (role != null)
            {
                foreach (var user in userManager.Users)
                {
                    if (user != null
                        && await userManager.IsInRoleAsync(user, role.Name))
                    {
                        names.Add(user.UserName);
                    }
                }
            }
            if (names.Count == 0)
            {
                output.Content.SetHtmlContent($"<li class=\"list-group-item\">Ingen fundet</li>");
            }
            else
            {
                string o = "";
                foreach (string name in names)
                {
                    o += $"<li class=\"list-group-item\">{name}</li>";
                }
                output.Content.SetHtmlContent(o);
            }
        }
    }
}
