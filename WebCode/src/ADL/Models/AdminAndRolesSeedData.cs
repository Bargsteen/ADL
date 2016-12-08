using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ADL.Models
{
    public class AdminAndRolesSeedData
    {
        public static async Task CreateTeacherStudentAndAdminRoles(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = configuration["Data:NecessaryRoles:Roles"].Split(',');
            foreach (string role in roles)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            UserManager<Person> userManager =
                serviceProvider.GetRequiredService<UserManager<Person>>();
            RoleManager<IdentityRole> roleManager =
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string username = configuration["Data:AdminUser:Username"];
            string email = configuration["Data:AdminUser:Email"];
            string password = configuration["Data:AdminUser:Password"];
            string[] roles = configuration["Data:NecessaryRoles:Roles"].Split(',');
            if (await userManager.FindByNameAsync(username) == null)
            {
                Person user = new Person
                {
                    UserName = username,
                    Email = email
                };
                IdentityResult result = await userManager
                    .CreateAsync(user, password);
                if (result.Succeeded)
                {
                    foreach (string role in roles)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }

                }
            }
        }
    }
}