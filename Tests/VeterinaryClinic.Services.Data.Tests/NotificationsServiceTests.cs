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
    public class NotificationsServiceTests
    {
        // TODO : List<T> GetOwnerNotifications<T>(string ownerId); List<T> GetVetNotifications<T>(string vetId);

        [Fact]
        public async Task CreateNotificationForOwnerShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var notificationsRepository = new EfDeletableEntityRepository<Notification>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));

            var service = new NotificationsService(notificationsRepository,vetsRepository);

            await service.CreateNotificationForOwnerAsync("testOwnerId123", "testContent");

            var count = notificationsRepository.All().Count();

            Assert.Equal(1, count);
        }

        [Fact]
        public async Task CreateNotificationForVetShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var notificationsRepository = new EfDeletableEntityRepository<Notification>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));

            var service = new NotificationsService(notificationsRepository, vetsRepository);

            await service.CreateNotificationForVetAsync("testVetId123", "testContent");

            var count = notificationsRepository.All().Count();

            Assert.Equal(1, count);
        }


    }
}
