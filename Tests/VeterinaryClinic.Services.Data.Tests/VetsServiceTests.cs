using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Repositories;
using Xunit;
using Microsoft.Extensions.Configuration;
using CloudinaryDotNet;
using VeterinaryClinic.Data.Common.Repositories;
using Moq;
using VeterinaryClinic.Web.ViewModels.Vets;

namespace VeterinaryClinic.Services.Data.Tests
{
    public class VetsServiceTests
    {
        [Fact]
        public async Task AddPetAsyncShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var mockedVetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository, mockedVetsServicesRepository.Object);
            IVetsService vetsService = new VetsService(vetsRepository, mockedVetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository, servicesRepository);

            await vetsService.AddVetAsync("testUserId", new AddVetInputModel { Email = "testEmail@gmail.com", FirstName = "testFirstName", LastName = "testLastName" , HireDate = DateTime.UtcNow , Password = "123456", Specialization = "testSpecialization" }, null);

            var vetsRepositoryActualCount = vetsRepository.All().Count();

            Assert.Equal(1, vetsRepositoryActualCount);
        }

        [Fact]
        public async Task GetCountShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var mockedVetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository, mockedVetsServicesRepository.Object);
            IVetsService vetsService = new VetsService(vetsRepository, mockedVetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository, servicesRepository);

            await vetsRepository.AddAsync(new Vet { UserId = "testUserId", FirstName = "testFirstName", LastName = "testLastName", HireDate = DateTime.UtcNow, Specialization = "testSpecialization" });
            await vetsRepository.SaveChangesAsync();

            var vetsRepositoryActualCount = vetsService.GetCount();

            Assert.Equal(1, vetsRepositoryActualCount);
        }
    }
}
