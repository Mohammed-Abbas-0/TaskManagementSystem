using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Infrastructures.Persistence
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var roles = new[] { "Admin", "User", "Manager" };
            foreach (var role in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // إضافة مستخدم Admin إذا لم يكن موجودًا
            var adminUser = await userManager.FindByEmailAsync("admin@example.com");
            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FirstName="Admin",
                    LastName="",
                    FullName="Admin"
                };
                await userManager.CreateAsync(adminUser, "12345Ee*");

                // إضافة دور Admin للمستخدم
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }


        }
    }
}
