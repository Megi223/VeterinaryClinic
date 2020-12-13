namespace VeterinaryClinic.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VeterinaryClinic.Data;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Repositories;
    using Xunit;

    public class OwnersServiceTests
    {
        [Fact]
        public async Task CreateOwnerAsyncShouldAddToDbCorrectEntity()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ownersRepository = new EfDeletableEntityRepository<Owner>(new ApplicationDbContext(options.Options));
            var reviewsRepository = new EfDeletableEntityRepository<Review>(new ApplicationDbContext(options.Options));

            var service = new OwnersService(ownersRepository, reviewsRepository);

            await service.CreateOwnerAsync(new ApplicationUser(), "firstName", "lastName", "someUrl", "city");

            var count = ownersRepository.All().Count();

            Assert.Equal(1, count);
            Assert.Equal("firstName", ownersRepository.All().First().FirstName);
            Assert.Equal("lastName", ownersRepository.All().First().LastName);
            Assert.Equal("someUrl", ownersRepository.All().First().ProfilePicture);
            Assert.Equal("city", ownersRepository.All().First().City);
        }

        [Fact]
        public async Task GetOwnerIdShouldReturnCorrectId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ownersRepository = new EfDeletableEntityRepository<Owner>(new ApplicationDbContext(options.Options));
            var reviewsRepository = new EfDeletableEntityRepository<Review>(new ApplicationDbContext(options.Options));

            var service = new OwnersService(ownersRepository, reviewsRepository);

            await ownersRepository.AddAsync(new Owner { Id = "testOwnerId123", UserId = "testUserId123", FirstName = "firstName", LastName = "lastName", ProfilePicture = "someUrl", City = "city" });
            await ownersRepository.SaveChangesAsync();

            var actualOwnerId = service.GetOwnerId("testUserId123");

            Assert.Equal("testOwnerId123", actualOwnerId);
        }

        [Fact]
        public async Task WriteReviewAsyncShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ownersRepository = new EfDeletableEntityRepository<Owner>(new ApplicationDbContext(options.Options));
            var reviewsRepository = new EfDeletableEntityRepository<Review>(new ApplicationDbContext(options.Options));

            var service = new OwnersService(ownersRepository, reviewsRepository);

            await service.WriteReviewAsync("testOwnerId123", new Web.ViewModels.Reviews.ReviewInputModel { Content = "testContent" });

            var count = reviewsRepository.All().Count();

            Assert.Equal(1, count);
        }
    }
}
