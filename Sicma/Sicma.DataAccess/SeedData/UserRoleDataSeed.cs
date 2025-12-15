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
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            string[] roles = { "SuperAdmin", "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new AppRole { 
                        Name= role,
                        NormalizedName = role.ToUpper()
                    });
                }
            }

            var superAdmin = new AppUser()
            {
                UserName = "superadminsicma",
                FullName = "Super Admin SICMA",
                Email = "test@test.com",
                CreatedDate = DateTime.Now,
                CreatedUserId = Guid.Parse("b7c6a2e4-1d6f-4a9c-9b3c-9d5d91c5a111"),
                IsActive = true
            };

            var user = await userManager.CreateAsync(superAdmin,"LePassword37");
            await userManager.AddToRoleAsync(superAdmin, "SuperAdmin");
        }
    }
}
