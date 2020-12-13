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

    public class RatingServiceTests
    {
        [Fact]
        public async Task SetRatingAsyncShouldAddToDbAndSetScoreWhenRatingIsNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ratingsRepository = new EfDeletableEntityRepository<Rating>(new ApplicationDbContext(options.Options));

            var service = new RatingService(ratingsRepository);

            await service.SetRatingAsync("testVetId", "testOwnerId", 2);

            var raringsRepositoryActualCount = ratingsRepository.All().Count();

            Assert.Equal(1, raringsRepositoryActualCount);
            Assert.Equal(2, ratingsRepository.All().First().Score);
            Assert.Equal("testOwnerId", ratingsRepository.All().First().OwnerId);
            Assert.Equal("testVetId", ratingsRepository.All().First().VetId);
        }

        [Fact]
        public async Task SetRatingAsyncShouldNotChangeCountAndShouldChangeScoreWhenRatingIsNotNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ratingsRepository = new EfDeletableEntityRepository<Rating>(new ApplicationDbContext(options.Options));

            var service = new RatingService(ratingsRepository);

            await ratingsRepository.AddAsync(new Rating { OwnerId = "testOwnerId", VetId = "testVetId", Score = 2 });
            await ratingsRepository.SaveChangesAsync();

            await service.SetRatingAsync("testVetId", "testOwnerId", 3);

            var raringsRepositoryActualCount = ratingsRepository.All().Count();

            Assert.Equal(1, raringsRepositoryActualCount);
            Assert.Equal(3, ratingsRepository.All().First().Score);
            Assert.Equal("testOwnerId", ratingsRepository.All().First().OwnerId);
            Assert.Equal("testVetId", ratingsRepository.All().First().VetId);
        }

        [Theory]
        [InlineData(3, 5, 4)]
        [InlineData(2, 5, 3.5)]
        public async Task GetAverageRatingShouldReturnCorrectResult(int firstScore, int secondScore, float expectedResult)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var ratingsRepository = new EfDeletableEntityRepository<Rating>(new ApplicationDbContext(options.Options));

            var service = new RatingService(ratingsRepository);

            await ratingsRepository.AddAsync(new Rating { OwnerId = "testOwnerId", VetId = "testVetId", Score = firstScore });
            await ratingsRepository.AddAsync(new Rating { OwnerId = "testOwnerId2", VetId = "testVetId", Score = secondScore });
            await ratingsRepository.SaveChangesAsync();

            float actualResult = service.GetAverageRatings("testVetId");

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
