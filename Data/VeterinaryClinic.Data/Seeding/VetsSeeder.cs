using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;

namespace VeterinaryClinic.Data.Seeding
{
    public class VetsSeeder : ISeeder
    {
        private const string VetUserName = "vet@gmail.com";

        private UserManager<ApplicationUser> usersManager;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.usersManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.Vets.Any())
            {
                await SeedVetAsync(this.usersManager, VetUserName);
            }
        }

        private static async Task SeedVetAsync(UserManager<ApplicationUser> userManager, string username)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                if (user.Vet == null)
                {
                    var vet = new Vet
                    {
                        UserId = user.Id,
                        FirstName = "Michael",
                        LastName = "Dimitrov",
                        ProfilePicture= "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684309/staff-2_sfkmhm.jpg",
                        HireDate = DateTime.UtcNow,
                    };

                    user.Vet = vet;
                }
            }
        }
    }
}
