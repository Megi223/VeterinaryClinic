namespace VeterinaryClinic.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;

    public class UsersSeeder : ISeeder
    {
        private const string AdminUserName = "admin@abv.com";
        private const string VetUserName = "vet@gmail.com";
        private const string OwnerUserName = "owner@mail.bg";
        private const string VetUserName2 = "vet2@gmail.com";

        private UserManager<ApplicationUser> usersManager;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.usersManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.Users.Any())
            {
                await SeedUserAsync(this.usersManager, AdminUserName);
                await SeedUserAsync(this.usersManager, VetUserName);
                await SeedUserAsync(this.usersManager, OwnerUserName);
                await SeedUserAsync(this.usersManager, VetUserName2);
            }
        }

        private static async Task SeedUserAsync(UserManager<ApplicationUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                user = new ApplicationUser()
                {
                    UserName = username,
                    Email = username,
                };

                IdentityResult result = new IdentityResult();

                if (username == VetUserName)
                {
                    result = await userManager.CreateAsync(user, "123456");
                }
                else if (username == VetUserName2)
                {
                    result = await userManager.CreateAsync(user, "123456");
                }
                else if (username == AdminUserName)
                {
                    result = await userManager.CreateAsync(user, "123456");
                }
                else
                {
                    result = await userManager.CreateAsync(user, "123456");
                }

                if (result.Succeeded)
                {
                    if (username == AdminUserName)
                    {
                        await userManager.AddToRoleAsync(user, GlobalConstants.AdministratorRoleName);
                    }
                    else if (username == OwnerUserName)
                    {
                        await userManager.AddToRoleAsync(user, GlobalConstants.OwnerRoleName);
                    }
                    else
                    {
                        await userManager.AddToRoleAsync(user, GlobalConstants.VetRoleName);
                    }
                }
            }
        }
    }
}
