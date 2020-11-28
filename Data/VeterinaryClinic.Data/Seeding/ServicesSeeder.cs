namespace VeterinaryClinic.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using VeterinaryClinic.Services;

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
