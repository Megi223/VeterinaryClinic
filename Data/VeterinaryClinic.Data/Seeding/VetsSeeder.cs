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
        private const string VetFirstName = "Michael";
        private const string VetLastName = "Dimitrov";
        private const string VetImageUrl = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684309/staff-2_sfkmhm.jpg";
        private const string VetSpecialization = "Cardiology";



        private const string VetUserName2 = "vet2@gmail.com";
        private const string VetFirstName2 = "Ivan";
        private const string VetLastName2 = "Penev";
        private const string VetImageUrl2 = "https://res.cloudinary.com/dpwroiluv/image/upload/v1604684309/staff-3_usfgz1.jpg";
        private const string VetSpecialization2 = "Dermatology";


        private UserManager<ApplicationUser> usersManager;

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            this.usersManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!dbContext.Vets.Any())
            {
                await SeedVetAsync(this.usersManager, VetUserName, VetFirstName, VetLastName, VetImageUrl,VetSpecialization);
                await SeedVetAsync(this.usersManager, VetUserName2, VetFirstName2, VetLastName2, VetImageUrl2,VetSpecialization2);
            }

        }

        private static async Task SeedVetAsync(UserManager<ApplicationUser> userManager, string username, string firstName, string lastName, string imageUrl,string vetSpecialization)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user != null)
            {
                if (user.Vet == null)
                {
                    var vet = new Vet
                    {
                        UserId = user.Id,
                        FirstName = firstName,
                        LastName = lastName,
                        ProfilePicture = imageUrl,
                        HireDate = DateTime.UtcNow,
                        Specialization=vetSpecialization,
                    };

                    user.Vet = vet;
                }
            }
        }
    }
}
