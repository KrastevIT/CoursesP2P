using CoursesP2P.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace CoursesP2P.App.Common
{
    public static class ApplicationBuilderAuthExtensions
    {
        private const string DefaultAdminPassword = "Test123@";
        private static readonly string Role = "Administrator";
        private static readonly string AdminUsername = "admin@example.com";
        private static readonly string AdminFirstName = "default";
        private static readonly string AdminLastName = "default";
        private static readonly string AdminEmail = "admin@example.com";

        public static async void SeedAdmin(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

                if (!await roleManager.RoleExistsAsync(Role))
                {
                    await roleManager.CreateAsync(new IdentityRole(Role));

                    var user = new User()
                    {
                        UserName = AdminUsername,
                        FirstName = AdminFirstName,
                        LastName = AdminLastName,
                        Email = AdminEmail,
                        EmailConfirmed = true,
                    };

                    await userManager.CreateAsync(user, DefaultAdminPassword);
                    await userManager.AddToRoleAsync(user, Role);
                }
            }
        }
    }
}
