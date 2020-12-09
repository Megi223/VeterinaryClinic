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

namespace VeterinaryClinic.Services.Data.Tests
{
    public class CommentsServiceTests
    {
        [Fact]
        public async Task CreateShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var commentsRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));

            var service = new CommentsService(commentsRepository);

            await service.AddAsync("testVetId123", "testOwnerId123", "testContent");

            var count = commentsRepository.All().Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task CreateShouldAddToDbWhenParentIdIsNotNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var commentsRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));

            var service = new CommentsService(commentsRepository);

            await service.AddAsync("testVetId123", "testOwnerId123", "testContent", 1);

            var count = commentsRepository.All().Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task IsInCorrectVetShouldReturnTrueWhenInTheSameVetProfile()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var commentsRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));

            var service = new CommentsService(commentsRepository);

            await commentsRepository.AddAsync(new Comment { Id=1,VetId = "testVetId123", OwnerId = "testOwnerId123", Content = "testContent" });
            await commentsRepository.SaveChangesAsync();
            var actualResult = service.IsInCorrectVetId(1, "testVetId123");

            Assert.True(actualResult);
        }

        [Fact]
        public async Task IsInCorrectVetShouldReturnFalseWhenNotInTheSameVetProfile()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var commentsRepository = new EfDeletableEntityRepository<Comment>(new ApplicationDbContext(options.Options));

            var service = new CommentsService(commentsRepository);

            await commentsRepository.AddAsync(new Comment { Id = 1, VetId = "testVetId123", OwnerId = "testOwnerId123", Content = "testContent" });
            await commentsRepository.AddAsync(new Comment { Id = 2, VetId = "testVetId1234", OwnerId = "testOwnerId123", Content = "testContent" });
            await commentsRepository.SaveChangesAsync();

            var actualResult = service.IsInCorrectVetId(1, "testVetId1234");

            Assert.False(actualResult);
        }
    }
}
