namespace VeterinaryClinic.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using VeterinaryClinic.Data;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Data.Repositories;
    using VeterinaryClinic.Services.Data.Tests.TestViewModels;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.Pets;
    using Xunit;

    public class PetsServiceTests
    {
        [Theory]
        [InlineData("1", "https://res.cloudinary.com/dpwroiluv/image/upload/v1606324362/dog_gxboja.png")]
        [InlineData("2", "https://res.cloudinary.com/dpwroiluv/image/upload/v1606324384/cat_y8yj67.png")]
        public async Task DeterminePhotoUrlShouldReturnCorrectPhotoUrl(string typeOfAnimal, string expected)
        {
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            var diagnoseRepository = new Mock<IDeletableEntityRepository<Diagnose>>();
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository.Object, cloudinaryService, diagnoseRepository.Object);
            var actualPhotoUrl = await service.DeterminePhotoUrl(null, typeOfAnimal);
            Assert.Equal(expected, actualPhotoUrl);
        }

        [Fact]
        public async Task AddPetAsyncShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var diagnoseRepository = new EfDeletableEntityRepository<Diagnose>(new ApplicationDbContext(options.Options));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository, cloudinaryService, diagnoseRepository);

            await service.AddPetAsync("testUserId", new Web.ViewModels.Pets.AddPetInputModel { Birthday = DateTime.UtcNow, Gender = "Male", IdentificationNumber = "123456", VetId = "testVetId123", Name = "test", Sterilised = "false", Type = "Dog", Weight = 2.5F }, null);

            var petsRepositoryActualCount = petsRepository.All().Count();

            Assert.Equal(1, petsRepositoryActualCount);
        }

        [Fact]
        public async Task AddPetAsyncShouldThrowExceptionWhenIdentificationNumberIsNotValid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var diagnoseRepository = new EfDeletableEntityRepository<Diagnose>(new ApplicationDbContext(options.Options));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository, cloudinaryService, diagnoseRepository);

            await petsRepository.AddAsync(new Pet { Name = "test", Birthday = DateTime.UtcNow, Gender = Gender.Male, IdentificationNumber = "123456", VetId = "testVetId123", Sterilised = false, Type = TypeOfAnimal.Dog, Weight = 2.5F });
            await petsRepository.SaveChangesAsync();

            await Assert.ThrowsAsync<ArgumentException>(async () => await service.AddPetAsync("testUserId", new Web.ViewModels.Pets.AddPetInputModel { Birthday = DateTime.UtcNow, Gender = "Male", IdentificationNumber = "123456", VetId = "testVetId123", Name = "test", Sterilised = "false", Type = "Dog", Weight = 2.5F }, null));
        }

        [Fact]
        public async Task GetCountForOwnerShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var diagnoseRepository = new EfDeletableEntityRepository<Diagnose>(new ApplicationDbContext(options.Options));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository, cloudinaryService, diagnoseRepository);

            await petsRepository.AddAsync(new Pet { Name = "test", Birthday = DateTime.UtcNow, Gender = Gender.Male, IdentificationNumber = "123456", VetId = "testVetId123", Sterilised = false, Type = TypeOfAnimal.Dog, Weight = 2.5F, OwnerId = "testOwnerId123" });
            await petsRepository.SaveChangesAsync();

            var actualCount = service.GetCountForOwner("testOwnerId123");
            Assert.Equal(1, actualCount);
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectEntity()
        {
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var diagnoseRepository = new EfDeletableEntityRepository<Diagnose>(new ApplicationDbContext(options.Options));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository, cloudinaryService, diagnoseRepository);

            await petsRepository.AddAsync(new Pet { Id = "testPetId", Name = "test", Birthday = new DateTime(2020, 12, 05), Gender = Gender.Male, IdentificationNumber = "123456", VetId = "testVetId123", Sterilised = false, Type = TypeOfAnimal.Dog, Weight = 2.5F, OwnerId = "testOwnerId123" });
            await petsRepository.SaveChangesAsync();

            var actualPet = service.GetById<PetViewModelTest>("testPetId");

            Assert.Equal("testPetId", actualPet.Id);
            Assert.Equal(new DateTime(2020, 12, 05), actualPet.Birthday);
            Assert.Equal(TypeOfAnimal.Dog, actualPet.Type);
            Assert.Equal(2.5F, actualPet.Weight);
            Assert.Equal("testOwnerId123", actualPet.OwnerId);
            Assert.Equal("testVetId123", actualPet.VetId);
            Assert.Equal(Gender.Male, actualPet.Gender);
            Assert.Equal("123456", actualPet.IdentificationNumber);
            Assert.Equal("test", actualPet.Name);
            Assert.False(actualPet.Sterilised);
        }

        [Fact]
        public void GetAllForAPageShouldReturnCorrectEntities()
        {
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());
            var diagnoseRepository = new Mock<IDeletableEntityRepository<Diagnose>>();
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository.Object, cloudinaryService, diagnoseRepository.Object);

            var actualPetsOnOnePage = service.GetAllForAPage<PetViewModelTest>(1, "testOwnerId123").ToList();

            Assert.Equal(3, actualPetsOnOnePage.Count());

            for (int i = 1; i <= actualPetsOnOnePage.Count(); i++)
            {
                Assert.Equal("testPetId" + i, actualPetsOnOnePage[i - 1].Id);
                Assert.Equal("test" + i, actualPetsOnOnePage[i - 1].Name);
                Assert.Equal("testOwnerId123", actualPetsOnOnePage[i - 1].OwnerId);
                Assert.Equal("testVetId123", actualPetsOnOnePage[i - 1].VetId);
            }
        }

        [Theory]
        [InlineData(1, 3)]
        [InlineData(2, 1)]
        [InlineData(3, 0)]
        public void GetAllForAPageShouldReturnCorrectCountForAPage(int page, int expectedCount)
        {
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());
            var diagnoseRepository = new Mock<IDeletableEntityRepository<Diagnose>>();
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository.Object, cloudinaryService, diagnoseRepository.Object);

            var actualPetsOnOnePage = service.GetAllForAPage<PetViewModelTest>(page, "testOwnerId123").ToList();

            Assert.Equal(expectedCount, actualPetsOnOnePage.Count());
        }

        [Fact]
        public void GetPetsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());
            var diagnoseRepository = new Mock<IDeletableEntityRepository<Diagnose>>();
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository.Object, cloudinaryService, diagnoseRepository.Object);

            var actualPets = service.GetPets<PetViewModelTest>("testOwnerId123").ToList();

            Assert.Equal(4, actualPets.Count());
        }

        [Fact]
        public void GetPetsShouldReturnCorrectEntities()
        {
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());
            var diagnoseRepository = new Mock<IDeletableEntityRepository<Diagnose>>();
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository.Object, cloudinaryService, diagnoseRepository.Object);

            var actualPets = service.GetPets<PetViewModelTest>("testOwnerId123").ToList();

            for (int i = 1; i <= actualPets.Count; i++)
            {
                Assert.Equal("testPetId" + i, actualPets[i - 1].Id);
                Assert.Equal("test" + i, actualPets[i - 1].Name);
                Assert.Equal("testOwnerId123", actualPets[i - 1].OwnerId);
                Assert.Equal("testVetId123", actualPets[i - 1].VetId);
            }
        }

        [Fact]
        public async Task SetDiagnoseAsyncShouldAddDiagnoseToDbAndSetItToCorrectPet()
        {
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());
            var diagnoseRepository = new Mock<IDeletableEntityRepository<Diagnose>>();
            diagnoseRepository.Setup(r => r.All())
                .Returns(new List<Diagnose> { new Diagnose { Name = "diagnoseName", Description = "testDiagnoseDesc" } }.AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository.Object, cloudinaryService, diagnoseRepository.Object);

            await service.SetDiagnoseAsync("testDiagnoseDesc", "diagnoseName", "testPetId1");

            Assert.Single(diagnoseRepository.Object.All());
            Assert.Equal("diagnoseName", petsRepository.Object.All().FirstOrDefault(x => x.Id == "testPetId1").Diagnose.Name);
        }

        [Fact]
        public async Task EditAsyncShouldSetNewValues()
        {
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());
            var diagnoseRepository = new Mock<IDeletableEntityRepository<Diagnose>>();
            diagnoseRepository.Setup(r => r.All())
                .Returns(new List<Diagnose> { new Diagnose { Name = "diagnoseName", Description = "testDiagnoseDesc" } }.AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository.Object, cloudinaryService, diagnoseRepository.Object);

            await service.EditAsync(new EditPetViewModel { Id = "testPetId1", Name = "test12", Sterilised = "true", VetId = "testVetIdNew", Weight = 2.9F });

            var changedPet = petsRepository.Object.All().Where(p => p.Id == "testPetId1").To<PetViewModelTest>().FirstOrDefault();
            Assert.Equal("test12", changedPet.Name);
            Assert.Equal("testVetIdNew", changedPet.VetId);
            Assert.Equal(2.9F, changedPet.Weight);
            Assert.True(changedPet.Sterilised);
        }

        [Fact]
        public async Task EditAsyncShouldKeepWhenNotChangedEntity()
        {
            AutoMapperConfig.RegisterMappings(typeof(PetViewModelTest).Assembly);
            var petsRepository = new Mock<IDeletableEntityRepository<Pet>>();
            petsRepository.Setup(r => r.All())
            .Returns(this.GetTestData().AsQueryable());
            var diagnoseRepository = new Mock<IDeletableEntityRepository<Diagnose>>();
            diagnoseRepository.Setup(r => r.All())
                .Returns(new List<Diagnose> { new Diagnose { Name = "diagnoseName", Description = "testDiagnoseDesc" } }.AsQueryable());
            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
            .AddJsonFile("appsettings.Development.json")
            .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository.Object, cloudinaryService, diagnoseRepository.Object);

            await service.EditAsync(new EditPetViewModel { Id = "testPetId1", Name = "test1", Sterilised = "false", VetId = "testVetId123", Weight = 2.5F });

            var notChangedPet = petsRepository.Object.All().Where(p => p.Id == "testPetId1").To<PetViewModelTest>().FirstOrDefault();
            Assert.Equal("test1", notChangedPet.Name);
            Assert.Equal("testVetId123", notChangedPet.VetId);
            Assert.Equal(2.5F, notChangedPet.Weight);
            Assert.False(notChangedPet.Sterilised);
        }

        [Fact]
        public async Task DeleteAsyncShouldDeleteFromDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var petsRepository = new EfDeletableEntityRepository<Pet>(new ApplicationDbContext(options.Options));
            var diagnoseRepository = new EfDeletableEntityRepository<Diagnose>(new ApplicationDbContext(options.Options));

            IConfiguration configuration = new ConfigurationBuilder().SetBasePath(@"C:\Users\Megi\Desktop\VeterinaryClinic\Web\VeterinaryClinic.Web")
           .AddJsonFile("appsettings.Development.json")
           .Build();
            Account account = new Account(
                configuration["Cloudinary:AppName"],
                configuration["Cloudinary:AppKey"],
                configuration["Cloudinary:AppSecret"]);
            Cloudinary cloudinary = new Cloudinary(account);
            ICloudinaryService cloudinaryService = new CloudinaryService(cloudinary);
            IPetsService service = new PetsService(petsRepository, cloudinaryService, diagnoseRepository);

            await petsRepository.AddAsync(new Pet { Id = "testPetId", Name = "test", Birthday = DateTime.UtcNow, Gender = Gender.Male, IdentificationNumber = "123456", VetId = "testVetId123", Sterilised = false, Type = TypeOfAnimal.Dog, Weight = 2.5F });
            await petsRepository.SaveChangesAsync();

            await service.DeleteAsync("testPetId");

            Assert.Equal(0, petsRepository.All().Count());
        }

        private List<Pet> GetTestData()
        {
            return new List<Pet>()
            {
                new Pet
                {
                    Id = "testPetId1",
                    Name = "test1",
                    Birthday = new DateTime(2020, 12, 05),
                    Gender = Gender.Male,
                    IdentificationNumber = "123456",
                    VetId = "testVetId123",
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
                    IdentificationNumber = "1234567",
                    VetId = "testVetId123",
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
                    IdentificationNumber = "1234568",
                    VetId = "testVetId123",
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
                    VetId = "testVetId123",
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
                    VetId = "testVetId123",
                    Sterilised = false,
                    Type = TypeOfAnimal.Dog,
                    Weight = 2.5F,
                    OwnerId = "testOwnerId1234",
                },
            };
        }
    }
}
