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
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Services.Data.Tests.TestViewModels;
using VeterinaryClinic.Data.Models.Enumerations;

namespace VeterinaryClinic.Services.Data.Tests
{
    public class VetsServiceTests
    {
        public VetsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(VetViewModelTest).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(VetsServicesViewModelTest).Assembly);
        }

        [Fact]
        public async Task AddVetAsyncShouldAddToDb()
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

        [Fact]
        public void GetAllForAPageShouldReturnCorrectEntities()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualVets = vetsService.GetAllForAPage<VetViewModelTest>(1).ToList();

            for (int i = 1; i <= actualVets.Count(); i++)
            {
                Assert.Equal("VetId" + i, actualVets[i - 1].Id);
                Assert.Equal("testFirstName" + i, actualVets[i - 1].FirstName);
                Assert.Equal("testLastName" + i, actualVets[i - 1].LastName);
                Assert.Equal("testSpecialization" + i, actualVets[i - 1].Specialization);
                Assert.Equal(new DateTime(2020, 04, 05), actualVets[i - 1].HireDate);
                Assert.Equal("testUserId" + i, actualVets[i - 1].UserId);
                Assert.Null(actualVets[i - 1].ProfilePicture);
            }
        }

        [Theory]
        [InlineData(1, 4)]
        [InlineData(2, 0)]
        public void GetAllForAPageShouldReturnCorrectCountForAPage(int page, int expectedCount)
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualVets = vetsService.GetAllForAPage<VetViewModelTest>(page).ToList();

            Assert.Equal(expectedCount, actualVets.Count());
        }

        [Fact]
        public void GetByIdShouldReturnCorrectEntity()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualVet = vetsService.GetById<VetViewModelTest>("VetId2");

            Assert.Equal("VetId2", actualVet.Id);
            Assert.Equal("testFirstName2", actualVet.FirstName);
            Assert.Equal("testLastName2", actualVet.LastName);
            Assert.Equal("testSpecialization2", actualVet.Specialization);
            Assert.Equal("testUserId2", actualVet.UserId);
            Assert.Equal(new DateTime(2020, 04, 05), actualVet.HireDate);
            Assert.Null(actualVet.ProfilePicture);
        }

        [Fact]
        public void GetAllShouldReturnCorrectEntities()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualVets = vetsService.GetAll<VetViewModelTest>().ToList();

            for (int i = 1; i <= actualVets.Count(); i++)
            {
                Assert.Equal("VetId" + i, actualVets[i - 1].Id);
                Assert.Equal("testFirstName" + i, actualVets[i - 1].FirstName);
                Assert.Equal("testLastName" + i, actualVets[i - 1].LastName);
                Assert.Equal("testSpecialization" + i, actualVets[i - 1].Specialization);
                Assert.Equal(new DateTime(2020, 04, 05), actualVets[i - 1].HireDate);
                Assert.Equal("testUserId" + i, actualVets[i - 1].UserId);
                Assert.Null(actualVets[i - 1].ProfilePicture);
            }
        }

        [Fact]
        public void GetAllShouldReturnCorrectCount()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualVets = vetsService.GetAll<VetViewModelTest>().ToList();

            Assert.Equal(4, actualVets.Count());
        }

        [Fact]
        public void GetServicesShouldReturnCorrectServices()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsServicesRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForVetsServicesRepository().AsQueryable());
            servicesRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForServicesRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var services = vetsService.GetServices("VetId1");

            Assert.Equal("testName1, testName2", services);
        }

        [Fact]
        public void GetVetIdShouldReturnCorrectVetId()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var vetId = vetsService.GetVetId("testUserId1");

            Assert.Equal("VetId1",vetId);
        }

        [Fact]
        public async Task DeterminePhotoUrlShoulReturnDefaulfPhotoUrlWhenInputIsNull()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualPhotoUrl = await vetsService.DeterminePhotoUrl(null);

            Assert.Equal("https://res.cloudinary.com/dpwroiluv/image/upload/v1606144918/default-profile-icon-16_vbh95n.png", actualPhotoUrl);
        }

        [Fact]
        public void GetPatientsForAPageShouldReturnCorrectEntities()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForPetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualPatients = vetsService.GetVetsPatientsForAPage<PetViewModelTest>("VetId1", 1).ToList();

            for (int i = 1; i <= actualPatients.Count(); i++)
            {
                Assert.Equal("testPetId" + i, actualPatients[i - 1].Id);
                Assert.Equal("test" + i, actualPatients[i - 1].Name);
                Assert.Equal(Gender.Male, actualPatients[i - 1].Gender);
                Assert.Equal("123456" + i, actualPatients[i - 1].IdentificationNumber);
                Assert.Equal("VetId1", actualPatients[i - 1].VetId);
                Assert.Equal(TypeOfAnimal.Dog, actualPatients[i - 1].Type);
                Assert.Equal(2.5F, actualPatients[i - 1].Weight);
                Assert.Equal("testOwnerId123", actualPatients[i - 1].OwnerId);
                Assert.False(actualPatients[i - 1].Sterilised);
            }
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 0)]
        public void GetPatientsForAPageShouldReturnCorrectCountForAPage(int page, int expectedCount)
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForPetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualPatients = vetsService.GetVetsPatientsForAPage<PetViewModelTest>("VetId2", page).ToList();

            Assert.Equal(expectedCount, actualPatients.Count());
        }

        [Fact]
        public void GetPatientsCountShouldReturnCorrectCount()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForPetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualPatientsCount = vetsService.GetPatientsCount("VetId1");

            Assert.Equal(3, actualPatientsCount);
        }

        [Fact]
        public void GetNameByIdShouldReturnCorrectFullName()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForVetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualName = vetsService.GetNameById("VetId1");

            Assert.Equal("testFirstName1 testLastName1", actualName);
        }

        [Fact]
        public async Task DeleteShouldSetIsDeletedToTrue()
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

            await vetsRepository.AddAsync(new Vet { Id = "testVetId", UserId = "testUserId", FirstName = "testFirstName", LastName = "testLastName", HireDate = DateTime.UtcNow, Specialization = "testSpecialization" });
            await vetsRepository.SaveChangesAsync();

            await vetsService.DeleteVet("testVetId");
            var vetsRepositoryActualCount = vetsRepository.All().Count();
            var vetsRepositoryActualCountWithDeleted = vetsRepository.AllWithDeleted().Count();

            Assert.Equal(0, vetsRepositoryActualCount);
            Assert.Equal(1, vetsRepositoryActualCountWithDeleted);
            Assert.True(vetsRepository.AllWithDeleted().First().IsDeleted);
        }

        [Fact]
        public async Task EditVetShouldEditCorrectlyWhenServicesInputIsNotNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository, vetsServicesRepository);
            IVetsService vetsService = new VetsService(vetsRepository, vetsServicesRepository, servicesService, cloudinaryService, petsRepository, servicesRepository);

            await vetsRepository.AddAsync(new Vet { Id = "testVetId", UserId = "testUserId", FirstName = "testFirstName", LastName = "testLastName", HireDate = DateTime.UtcNow, Specialization = "testSpecialization" });
            await vetsRepository.SaveChangesAsync();

            await vetsService.EditVet(new EditVetInputModel { Id = "testVetId", Specialization = "newSpecialiation", ServicesInput = new List<string> { "1", "2" } });
            var editedVet = vetsRepository.All().FirstOrDefault(x => x.Id == "testVetId");

            var actualVetsServicesCount = vetsServicesRepository.All().Where(x => x.VetId == "testVetId").Count();

            Assert.Equal("testVetId", editedVet.Id);
            Assert.Equal("newSpecialiation", editedVet.Specialization);
            Assert.Equal(2, actualVetsServicesCount);
        }

        [Fact]
        public async Task EditVetShouldEditCorrectlyWhenServicesInputIsNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));
            var servicesRepository = new EfDeletableEntityRepository<Service>(new ApplicationDbContext(options.Options));
            var vetsServicesRepository = new EfDeletableEntityRepository<VetsServices>(new ApplicationDbContext(options.Options));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository, vetsServicesRepository);
            IVetsService vetsService = new VetsService(vetsRepository, vetsServicesRepository, servicesService, cloudinaryService, petsRepository, servicesRepository);

            await vetsRepository.AddAsync(new Vet { Id = "testVetId", UserId = "testUserId", FirstName = "testFirstName", LastName = "testLastName", HireDate = DateTime.UtcNow, Specialization = "testSpecialization" });
            await vetsRepository.SaveChangesAsync();

            await vetsServicesRepository.AddAsync(new VetsServices { VetId = "testVetId", ServiceId = "1" });
            await vetsServicesRepository.SaveChangesAsync();

            await vetsService.EditVet(new EditVetInputModel { Id = "testVetId", Specialization = "newSpecialiation", ServicesInput = null});
            var editedVet = vetsRepository.All().FirstOrDefault(x => x.Id == "testVetId");

            var actualVetsServicesCount = vetsServicesRepository.All().Where(x => x.VetId == "testVetId").Count();

            Assert.Equal("testVetId", editedVet.Id);
            Assert.Equal("newSpecialiation", editedVet.Specialization);
            Assert.Equal(0, actualVetsServicesCount);
        }

        [Fact]
        public void GetServicesGenericShouldReturnCorrectEntities()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsServicesRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForVetsServicesRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualServices = vetsService.GetServices<VetsServicesViewModelTest>("VetId1").ToList();

            for (int i = 1; i <= actualServices.Count(); i++)
            {
                Assert.Equal(i.ToString(), actualServices[i - 1].ServiceId);
                Assert.Equal("VetId1", actualServices[i - 1].VetId);
            }
        }

        [Fact]
        public void GetServicesGenericShouldReturnCorrectCount()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            vetsServicesRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForVetsServicesRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualServicesCount = vetsService.GetServices<VetsServicesViewModelTest>("VetId1").Count();

            Assert.Equal(2, actualServicesCount);
        }

        [Fact]
        public void GetPatientsGenericShouldReturnCorrectEntities()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForPetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualPatients = vetsService.GetVetsPatients<PetViewModelTest>("VetId1").ToList();

            for (int i = 1; i <= actualPatients.Count(); i++)
            {
                Assert.Equal("testPetId" + i, actualPatients[i - 1].Id);
                Assert.Equal("test" + i, actualPatients[i - 1].Name);
                Assert.Equal(Gender.Male, actualPatients[i - 1].Gender);
                Assert.Equal("123456" + i, actualPatients[i - 1].IdentificationNumber);
                Assert.Equal("VetId1", actualPatients[i - 1].VetId);
                Assert.Equal(TypeOfAnimal.Dog, actualPatients[i - 1].Type);
                Assert.Equal(2.5F, actualPatients[i - 1].Weight);
                Assert.Equal("testOwnerId123", actualPatients[i - 1].OwnerId);
                Assert.False(actualPatients[i - 1].Sterilised);
            }
        }

        [Fact]
        public void GetPatientsGenericShouldReturnCorrectCount()
        {
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            var vetsServicesRepository = new Mock<IDeletableEntityRepository<VetsServices>>();
            var servicesRepository = new Mock<IDeletableEntityRepository<Service>>();
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestDataForPetRepository().AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IServicesService servicesService = new ServicesService(servicesRepository.Object, vetsServicesRepository.Object);

            IVetsService vetsService = new VetsService(vetsRepository.Object, vetsServicesRepository.Object, servicesService, cloudinaryService, petsRepository.Object, servicesRepository.Object);

            var actualPatientsCount = vetsService.GetVetsPatients<PetViewModelTest>("VetId1").Count();

            Assert.Equal(3, actualPatientsCount);
        }

        private List<Pet> GetTestDataForPetRepository()
        {
            return new List<Pet>
            {
                new Pet
                {
                    Id = "testPetId1",
                    Name = "test1",
                    Birthday = new DateTime(2020, 12, 05),
                    Gender = Gender.Male,
                    IdentificationNumber = "1234561",
                    VetId = "VetId1",
                    Sterilised = false,
                    Type = TypeOfAnimal.Dog,
                    Weight = 2.5F,
                    OwnerId = "testOwnerId123",
                },
                new Pet
                {
                    Id = "testPetId2",
                    Name = "test2",
                    Birthday = new DateTime(2020, 12, 05),
                    Gender = Gender.Male,
                    IdentificationNumber = "1234562",
                    VetId = "VetId1",
                    Sterilised = false,
                    Type = TypeOfAnimal.Dog,
                    Weight = 2.5F,
                    OwnerId = "testOwnerId123",
                },
                new Pet
                {
                    Id = "testPetId3",
                    Name = "test3",
                    Birthday = new DateTime(2020, 12, 05),
                    Gender = Gender.Male,
                    IdentificationNumber = "1234563",
                    VetId = "VetId1",
                    Sterilised = false,
                    Type = TypeOfAnimal.Dog,
                    Weight = 2.5F,
                    OwnerId = "testOwnerId123",
                },
                new Pet
                {
                    Id = "testPetId4",
                    Name = "test4",
                    Birthday = new DateTime(2020, 12, 05),
                    Gender = Gender.Male,
                    IdentificationNumber = "1234569",
                    VetId = "VetId2",
                    Sterilised = false,
                    Type = TypeOfAnimal.Dog,
                    Weight = 2.5F,
                    OwnerId = "testOwnerId123",
                },
                new Pet
                {
                    Id = "testPetId5",
                    Name = "test5",
                    Birthday = new DateTime(2020, 12, 05),
                    Gender = Gender.Male,
                    IdentificationNumber = "12345610",
                    VetId = "VetId2",
                    Sterilised = false,
                    Type = TypeOfAnimal.Dog,
                    Weight = 2.5F,
                    OwnerId = "testOwnerId1234",
                },
            };
        }

        private List<Service> GetTestDataForServicesRepository()
        {
            return new List<Service>()
            {
                new Service
                {
                    Id = 1,
                    Name = "testName1",
                },
                new Service
                {
                    Id = 2,
                    Name = "testName2",
                },
            };
        }

        private List<VetsServices> GetTestDataForVetsServicesRepository()
        {
            return new List<VetsServices>
            {
                new VetsServices
                {
                    VetId = "VetId1",
                    ServiceId = "1",
                },
                new VetsServices
                {
                    VetId = "VetId1",
                    ServiceId = "2",
                },
                new VetsServices
                {
                    VetId = "VetId2",
                    ServiceId = "2",
                },
            };
        }

        private List<Vet> GetTestDataForVetRepository()
        {
            return new List<Vet>
            {
                new Vet
                {
                    Id = "VetId1",
                    FirstName = "testFirstName1",
                    LastName = "testLastName1",
                    Specialization = "testSpecialization1",
                    HireDate = new DateTime(2020, 04, 05),
                    UserId = "testUserId1",
                    ProfilePicture = null,
                },
                new Vet
                {
                    Id = "VetId2",
                    FirstName = "testFirstName2",
                    LastName = "testLastName2",
                    Specialization = "testSpecialization2",
                    HireDate = new DateTime(2020, 04, 05),
                    UserId = "testUserId2",
                    ProfilePicture = null,
                },
                new Vet
                {
                    Id = "VetId3",
                    FirstName = "testFirstName3",
                    LastName = "testLastName3",
                    Specialization = "testSpecialization3",
                    HireDate = new DateTime(2020, 04, 05),
                    UserId = "testUserId3",
                    ProfilePicture = null,
                },
                new Vet
                {
                    Id = "VetId4",
                    FirstName = "testFirstName4",
                    LastName = "testLastName4",
                    Specialization = "testSpecialization4",
                    HireDate = new DateTime(2020, 04, 05),
                    UserId = "testUserId4",
                    ProfilePicture = null,
                },
            };
        }
    }
}
