using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Sicma.Entities;

namespace Sicma.DataAccess.SeedData
{
    public static class UserRoleDataSeed
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roles = { "SuperAdmin", "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var superAdmin = new AppUser()
            {                
                UserName = "superadminsicma",
                Email = "test@test.com",
                CreatedDate = DateTime.Now,
                CreatedUserId = "1",
                IsActive = true
            };

            var user = await userManager.CreateAsync(superAdmin,"LePassword37");
            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
        }
    }
}
