namespace VeterinaryClinic.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Moq;
    using VeterinaryClinic.Data;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Repositories;
    using VeterinaryClinic.Services.Data.Tests.TestViewModels;
    using VeterinaryClinic.Services.Mapping;
    using Xunit;

    public class NotificationsServiceTests
    {
        [Fact]
        public async Task CreateNotificationForOwnerShouldAddToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var notificationsRepository = new EfDeletableEntityRepository<Notification>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));

            var service = new NotificationsService(notificationsRepository, vetsRepository);

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

        [Fact]
        public void GetOwnerNotificationsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(NotificationViewModelTest).Assembly);
            var notificationsRepository = new Mock<IDeletableEntityRepository<Notification>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            notificationsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForNotificationRepository().AsQueryable());

            INotificationsService notificationsService = new NotificationsService(notificationsRepository.Object, vetsRepository.Object);

            var actualOwnerNotifications = notificationsService.GetOwnerNotifications<NotificationViewModelTest>("OwnerId1");

            var actualCount = actualOwnerNotifications.Count();

            Assert.Equal(2, actualCount);
        }

        [Fact]
        public void GetOwnerNotificationsShouldReturnCorrectEntities()
        {
            AutoMapperConfig.RegisterMappings(typeof(NotificationViewModelTest).Assembly);
            var notificationsRepository = new Mock<IDeletableEntityRepository<Notification>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            notificationsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForNotificationRepository().AsQueryable());

            INotificationsService notificationsService = new NotificationsService(notificationsRepository.Object, vetsRepository.Object);

            var actualOwnerNotifications = notificationsService.GetOwnerNotifications<NotificationViewModelTest>("OwnerId1").ToList();

            for (int i = 1; i <= actualOwnerNotifications.Count(); i++)
            {
                Assert.Equal("VetId" + i, actualOwnerNotifications[i - 1].VetId);
                Assert.Equal("OwnerId1", actualOwnerNotifications[i - 1].OwnerId);
                Assert.Equal("Content" + i, actualOwnerNotifications[i - 1].Content);
            }
        }

        [Fact]
        public void GetVetNotificationsShouldReturnCorrectCount()
        {
            AutoMapperConfig.RegisterMappings(typeof(NotificationViewModelTest).Assembly);
            var notificationsRepository = new Mock<IDeletableEntityRepository<Notification>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            notificationsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForNotificationRepository().AsQueryable());

            INotificationsService notificationsService = new NotificationsService(notificationsRepository.Object, vetsRepository.Object);

            var actualVetNotifications = notificationsService.GetVetNotifications<NotificationViewModelTest>("VetId2");

            var actualCount = actualVetNotifications.Count();

            Assert.Equal(2, actualCount);
        }

        [Fact]
        public void GetVetNotificationsShouldReturnCorrectEntities()
        {
            AutoMapperConfig.RegisterMappings(typeof(NotificationViewModelTest).Assembly);
            var notificationsRepository = new Mock<IDeletableEntityRepository<Notification>>();
            var vetsRepository = new Mock<IDeletableEntityRepository<Vet>>();
            notificationsRepository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForNotificationRepository().AsQueryable());

            INotificationsService notificationsService = new NotificationsService(notificationsRepository.Object, vetsRepository.Object);

            var actualVetNotifications = notificationsService.GetVetNotifications<NotificationViewModelTest>("VetId1").ToList();

            for (int i = 1; i <= actualVetNotifications.Count(); i++)
            {
                Assert.Equal("VetId1", actualVetNotifications[i - 1].VetId);
                Assert.Equal("OwnerId1", actualVetNotifications[i - 1].OwnerId);
                Assert.Equal("Content1", actualVetNotifications[i - 1].Content);
            }
        }

        [Fact]
        public async Task DeleteShouldRemoveEntityFromDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var notificationsRepository = new EfDeletableEntityRepository<Notification>(new ApplicationDbContext(options.Options));
            var vetsRepository = new EfDeletableEntityRepository<Vet>(new ApplicationDbContext(options.Options));

            var service = new NotificationsService(notificationsRepository, vetsRepository);

            await notificationsRepository.AddAsync(new Notification
            {
                Id = 1,
                OwnerId = "OwnerId1",
                VetId = "VetId1",
                Content = "Content1",
            });
            await notificationsRepository.AddAsync(new Notification
            {
                Id = 2,
                OwnerId = "OwnerId1",
                VetId = "VetId2",
                Content = "Content2",
            });
            await notificationsRepository.AddAsync(new Notification
            {
                Id = 3,
                OwnerId = "OwnerId2",
                VetId = "VetId2",
                Content = "Content3",
            });
            await notificationsRepository.SaveChangesAsync();

            await service.Delete(1);

            var count = notificationsRepository.All().Count();

            Assert.Equal(2, count);
        }

        private List<Notification> GetTestDataForNotificationRepository()
        {
            return new List<Notification>
            {
                new Notification
                {
                    Id = 1,
                    OwnerId = "OwnerId1",
                    VetId = "VetId1",
                    Content = "Content1",
                },
                new Notification
                {
                    Id = 2,
                    OwnerId = "OwnerId1",
                    VetId = "VetId2",
                    Content = "Content2",
                },
                new Notification
                {
                    Id = 3,
                    OwnerId = "OwnerId2",
                    VetId = "VetId2",
                    Content = "Content3",
                },
            };
        }
    }
}
