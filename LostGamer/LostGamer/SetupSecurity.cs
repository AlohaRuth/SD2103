using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostGamer
{
    public static class SetupSecurity
    {
        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            IdentityUser admin = userManager.FindByEmailAsync("admin@lostgamer.com").Result;

            if (admin == null)
            {
                IdentityUser sysadmin = new IdentityUser();
                sysadmin.Email = "admin@lostgamer.com";
                sysadmin.UserName = "admin@lostgamer.com";

                IdentityResult result = userManager.CreateAsync(sysadmin, "@Admin1").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(sysadmin, "Administrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "NormalUser";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.
                CreateAsync(role).Result;
            }
        }
    }
}
