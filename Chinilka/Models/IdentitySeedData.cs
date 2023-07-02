using Chinilka.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Chinilka.Models
{
    public class IdentitySeedData
    {
        private const string adminLogin = "Admin";
        private const string adminName = "Admin";
        private const string adminPassword = "Admin123!";
        private const string adminRole = "Admin";
        private const string userRole = "User";

        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            AppIdentityDbContext context = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<AppIdentityDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            await CreateRolesAsync(app);
            await CreateAdminAsync(app);
            await CreateTestUsersAsync(app);
        }

        private static async Task CreateRolesAsync(IApplicationBuilder app)
        {
            RoleManager<IdentityRole> roleManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<RoleManager<IdentityRole>>();

            var adminRoleExist = await roleManager.RoleExistsAsync(adminRole);
            var userRoleExist = await roleManager.RoleExistsAsync(userRole);

            if (!adminRoleExist)
                await roleManager.CreateAsync(new IdentityRole(adminRole));

            if (!userRoleExist)
                await roleManager.CreateAsync(new IdentityRole(userRole));
        }

        private static async Task CreateAdminAsync(IApplicationBuilder app)
        {
            UserManager<ChinilkaUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<ChinilkaUser>>();

            ChinilkaUser admin = await userManager.FindByNameAsync(adminName);

            if (admin == null)
            {
                admin = new ChinilkaUser();
                admin.Login = adminLogin;
                admin.UserName = adminName;
                admin.Email = "admin@example.com";
                admin.PhoneNumber = "8-928-711-18-21";
                await userManager.CreateAsync(admin, adminPassword);
                await userManager.AddToRoleAsync(admin, adminRole);
            }
        }

        private static async Task CreateTestUsersAsync(IApplicationBuilder app)
        {
            UserManager<ChinilkaUser> userManager = app.ApplicationServices
                .CreateScope().ServiceProvider
                .GetRequiredService<UserManager<ChinilkaUser>>();

            var users = await userManager.GetUsersInRoleAsync(userRole);

            if (!users.Any())
            {
                var random = new Random();
                for (int i = 1; i <= 100; i++)
                {
                    var user = new ChinilkaUser
                    {
                        Login = $"User{i}",
                        UserName = $"User{i}",
                        PhoneNumber = $"8-{random.Next(400, 999)}-{random.Next(100, 999)}-{random.Next(10, 99)}-{random.Next(10, 99)}",
                        Email = $"user{i}@mail.ru"
                    };
                    await userManager.CreateAsync(user, $"User{i}!");
                    await userManager.AddToRoleAsync(user, userRole);
                }
            }
        }
    }
}
