using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Website.Data.Models;
using static Website.Common.ApplicationConstants;

namespace Website.Infrastructure.Extensions
{
    public static class SeedRolesExtension
    {
        public static IApplicationBuilder SeedRoles(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateAsyncScope();
            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            RoleManager<IdentityRole<Guid>>? roleManager = serviceProvider
                .GetService<RoleManager<IdentityRole<Guid>>>();
            IUserStore<ApplicationUser>? userStore = serviceProvider
                .GetService<IUserStore<ApplicationUser>>();
            UserManager<ApplicationUser>? userManager = serviceProvider
                .GetService<UserManager<ApplicationUser>>();

            if (roleManager == null)
            {
                throw new ArgumentNullException(nameof(roleManager),
                    $"Service for {typeof(RoleManager<IdentityRole<Guid>>)} cannot be obtained!");
            }

            if (userStore == null)
            {
                throw new ArgumentNullException(nameof(userStore),
                    $"Service for {typeof(IUserStore<ApplicationUser>)} cannot be obtained!");
            }

            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager),
                    $"Service for {typeof(UserManager<ApplicationUser>)} cannot be obtained!");
            }

            Task.Run(async () =>
            {
                await AddRoleAsync(roleManager, AdminRoleName);
                await AddRoleAsync(roleManager, UserRoleName);
                return app;
            })
                .GetAwaiter()
                .GetResult();

            return app;
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole<Guid>> roleManager, string roleName)
        {
            bool roleExists = await roleManager.RoleExistsAsync(roleName);
            IdentityRole<Guid>? role = null;
            if (!roleExists)
            {
                role = new IdentityRole<Guid>(roleName);

                IdentityResult result = await roleManager.CreateAsync(role);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"Error occurred while creating the {roleName} role!");
                }
            }
        }
    }
}
