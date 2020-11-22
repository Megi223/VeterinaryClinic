using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;

namespace VeterinaryClinic.Data.Seeding
{
    public class UsersSeeder : ISeeder
    {
        private const string AdminUserName = "admin@abv.com";
        private const string VetUserName = "vet@gmail.com";
        private const string OwnerUserName = "owner@mail.bg";

        private UserManager<ApplicationUser> usersManager;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.usersManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.Users.Any())
            {
                await SeedUserAsync(this.usersManager, AdminUserName);
                await SeedUserAsync(this.usersManager, VetUserName);
                await SeedUserAsync(this.usersManager, OwnerUserName);
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
                        await userManager.AddToRoleAsync(user, GlobalConstants.VeterinarianRoleName);
                    }
                }
            }
        }
    }
}
