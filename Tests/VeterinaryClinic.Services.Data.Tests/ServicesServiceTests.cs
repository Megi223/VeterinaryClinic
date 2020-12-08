using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Web.ViewModels.Services;
using VeterinaryClinic.Web.ViewModels.Vets;
using Xunit;

namespace VeterinaryClinic.Services.Data.Tests
{
    public class ServicesServiceTests
    {
        [Fact]
        public async Task GetNameByIdShouldReturnCorrectName()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));

            await servicesRepository.AddAsync(new Service { Name="test",Description="testDesc" });
            await servicesRepository.SaveChangesAsync();

            var servicesService = new ServicesService(servicesRepository,vetsServicesRepository);

            var actualName = servicesService.GetNameById(1);

            Assert.Equal("test", actualName);
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectEntity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));

            await servicesRepository.AddAsync(new Service { Name = "test", Description = "testDesc" });
            await servicesRepository.SaveChangesAsync();

            var servicesService = new ServicesService(servicesRepository, vetsServicesRepository);

            AutoMapperConfig.RegisterMappings(typeof(ServiceDetailsViewModel).Assembly);
            var service = servicesService.GetById<ServiceDetailsViewModel>(1);

            Assert.Equal("test", service.Name);
            Assert.Equal("testDesc", service.Description);
            Assert.Null(service.ImageUrl);
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectEntities()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));

            await servicesRepository.AddAsync(new Service { Name = "test1", Description = "testDesc1" });
            await servicesRepository.AddAsync(new Service { Name = "test2", Description = "testDesc2" });
            await servicesRepository.AddAsync(new Service { Name = "test3", Description = "testDesc3" });
            await servicesRepository.SaveChangesAsync();

            var servicesService = new ServicesService(servicesRepository, vetsServicesRepository);

            AutoMapperConfig.RegisterMappings(typeof(ServiceDetailsViewModel).Assembly);
            var services = servicesService.GetAll<ServiceDetailsViewModel>().ToList();

            for (int i = 1; i <= services.Count(); i++)
            {
                Assert.Equal("test" + i, services[i-1].Name);
                Assert.Equal("testDesc" + i, services[i-1].Description);
                Assert.Null(services[i-1].ImageUrl);
            }
        }

        [Fact]
        public async Task GetAllShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));

            await servicesRepository.AddAsync(new Service { Name = "test1", Description = "testDesc1" });
            await servicesRepository.AddAsync(new Service { Name = "test2", Description = "testDesc2" });
            await servicesRepository.AddAsync(new Service { Name = "test3", Description = "testDesc3" });
            await servicesRepository.SaveChangesAsync();

            var servicesService = new ServicesService(servicesRepository, vetsServicesRepository);

            AutoMapperConfig.RegisterMappings(typeof(ServiceDetailsViewModel).Assembly);
            var services = servicesService.GetAll<ServiceDetailsViewModel>().ToList();

            var expectedCount = 3;
            var actualCount = services.Count();

            Assert.Equal(expectedCount, actualCount);

        }

        [Fact]
        public async Task GetAllServicesWhichAVetDoesNotHaveShouldReturnCorrectEntity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));
            var servicesService = new ServicesService(servicesRepository, vetsServicesRepository);
            
            await servicesRepository.AddAsync(new Service { Name = "test1", Description = "testDesc1" });
            await servicesRepository.AddAsync(new Service { Name = "test2", Description = "testDesc2" });
            await servicesRepository.AddAsync(new Service { Name = "test3", Description = "testDesc3" });
            await servicesRepository.SaveChangesAsync();

            await vetsServicesRepository.AddAsync(new VetsServices { ServiceId = 1.ToString(), VetId = "dspps-4592-sjfis-sj" });
            await vetsServicesRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(ServiceViewModel).Assembly);

            var services = servicesService.GetAllServicesWhichAVetDoesNotHave<ServiceViewModel>("dspps-4592-sjfis-sj").ToList();

            for (int i = 1; i <= services.Count(); i++)
            {
                Assert.Equal(i+1, services[i - 1].Id);
            }

        }

        [Fact]
        public async Task GetAllServicesWhichAVetDoesNotHaveShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));
            var servicesService = new ServicesService(servicesRepository, vetsServicesRepository);

            await servicesRepository.AddAsync(new Service { Name = "test1", Description = "testDesc1" });
            await servicesRepository.AddAsync(new Service { Name = "test2", Description = "testDesc2" });
            await servicesRepository.AddAsync(new Service { Name = "test3", Description = "testDesc3" });
            await servicesRepository.SaveChangesAsync();

            await vetsServicesRepository.AddAsync(new VetsServices { ServiceId = 1.ToString(), VetId = "dspps-4592-sjfis-sj" });
            await vetsServicesRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(ServiceViewModel).Assembly);

            var services = servicesService.GetAllServicesWhichAVetDoesNotHave<ServiceViewModel>("dspps-4592-sjfis-sj").ToList();

            Assert.Equal(2, services.Count);
        }

        [Fact]
        public async Task AddServiceToVetShouldAddCorrectly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));
            var servicesService = new ServicesService(servicesRepository, vetsServicesRepository);

            await servicesRepository.AddAsync(new Service { Name = "test1", Description = "testDesc1" });
            await servicesRepository.AddAsync(new Service { Name = "test2", Description = "testDesc2" });
            await servicesRepository.AddAsync(new Service { Name = "test3", Description = "testDesc3" });
            await servicesRepository.SaveChangesAsync();

            AutoMapperConfig.RegisterMappings(typeof(ServiceViewModel).Assembly);

            await servicesService.AddServiceToVet(new AddServiceToVetInputModel { VetId = "dspps-4592-sjfis", Services = new List<int> { 1, 2 } });

            var expectedCount = 2;
            var actualCount = vetsServicesRepository.All().Where(x => x.VetId == "dspps-4592-sjfis").Count();

            Assert.Equal(expectedCount, actualCount);
        }



    }
}
