using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Services;

namespace VeterinaryClinic.Data.Seeding
{
    public class ServicesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Services.Any())
            {
                var service = (IServiceScraperService)serviceProvider.GetService(typeof(IServiceScraperService));
                await service.PopulateDbWithServices();
            }
        }
    }
}
