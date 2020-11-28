namespace VeterinaryClinic.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.DependencyInjection;
    using VeterinaryClinic.Data.Models;

    public class OwnersSeeder : ISeeder
    {
        private const string OwnerUserName = "owner@mail.bg";

        private UserManager<ApplicationUser> usersManager;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.usersManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            if (!dbContext.Owners.Any())
            {
                await SeedOwnerAsync(this.usersManager, OwnerUserName);
            }
        }

        private static async Task SeedOwnerAsync(UserManager<ApplicationUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                if (user.Owner == null)
                {
                    var owner = new Owner
                    {
                        UserId = user.Id,
                        City = "Sofia",
                        FirstName = "Anna",
                        LastName = "Ivanova",
                        ProfilePicture = "https://res.cloudinary.com/dpwroiluv/image/upload/v1606080381/anna-owner_tb09ym.jpg",
                    };

                    user.Owner = owner;
                }
            }
        }
    }
}
