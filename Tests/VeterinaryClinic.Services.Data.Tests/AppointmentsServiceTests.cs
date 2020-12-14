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
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Data.Repositories;
    using VeterinaryClinic.Services.Data.Tests.TestViewModels;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.Appointments;
    using Xunit;

    public class AppointmentsServiceTests
    {
        public AppointmentsServiceTests()
        {
            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);
        }

        [Fact]
        public async Task CreateAppointmentAsyncShouldAddAppointmentToDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var appointmentsRepository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            var appointmentsService = new AppointmentsService(appointmentsRepository);

            await appointmentsService.CreateAppointmentAsync(new RequestAppointmentViewModel { Date = DateTime.UtcNow, PetId = "testPetId123", Subject = "testSubject", Time = DateTime.UtcNow, VetId = "testVetId123" }, "testOwnerId123");

            Assert.Equal(1, appointmentsRepository.All().Count());
        }

        [Fact]
        public async Task IsAcceptedByVetShouldBeFalseUponInitializationOfAppointment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var appointmentsRepository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            var appointmentsService = new AppointmentsService(appointmentsRepository);

            await appointmentsService.CreateAppointmentAsync(new RequestAppointmentViewModel { Date = DateTime.UtcNow, PetId = "testPetId123", Subject = "testSubject", Time = DateTime.UtcNow, VetId = "testVetId123" }, "testOwnerId123");

            Assert.False(appointmentsRepository.All().First().IsAcceptedByVet);
        }

        [Fact]
        public async Task IsCancelledByOwnerShouldBeFalseUponInitializationOfAppointment()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var appointmentsRepository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            var appointmentsService = new AppointmentsService(appointmentsRepository);

            await appointmentsService.CreateAppointmentAsync(new RequestAppointmentViewModel { Date = DateTime.UtcNow, PetId = "testPetId123", Subject = "testSubject", Time = DateTime.UtcNow, VetId = "testVetId123" }, "testOwnerId123");

            Assert.False(appointmentsRepository.All().First().IsCancelledByOwner);
        }

        [Fact]
        public void GetVetPendingAppointmentsShouldReturnCorrectCountOfAppointments()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestData().AsQueryable());

            IAppointmentsService service = new AppointmentsService(repository.Object);

            List<AppointmentViewModelTest> pendingAppointments = service.GetVetPendingAppointments<AppointmentViewModelTest>("testVetId123").ToList();

            Assert.Equal(3, pendingAppointments.Count());
        }

        [Fact]
        public void GetVetPendingAppointmentsShouldReturnCorrectEntities()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestData().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            IAppointmentsService service = new AppointmentsService(repository.Object);

            List<AppointmentViewModelTest> pendingAppointments = service.GetVetPendingAppointments<AppointmentViewModelTest>("testVetId123").ToList();

            for (int i = 1; i <= pendingAppointments.Count(); i++)
            {
                Assert.Equal("testSubject" + i, pendingAppointments[i - 1].Subject);
                Assert.Equal("testPetId123", pendingAppointments[i - 1].PetId);
                Assert.Equal("testVetId123", pendingAppointments[i - 1].VetId);
                Assert.Equal("testOwnerId123", pendingAppointments[i - 1].OwnerId);
                Assert.Equal(new DateTime(2020, 12, 13, 14, 30, 30), pendingAppointments[i - 1].StartTime);
            }
        }

        [Fact]
        public async Task AcceptShouldChangeValueOfIsAcceptedByVetToTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);

            var service = new AppointmentsService(repository);

            await service.AcceptAsync("testId1");

            Assert.True(repository.All().First().IsAcceptedByVet);
        }

        [Fact]
        public async Task EndShouldEndAppointmentProperly()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);

            var service = new AppointmentsService(repository);

            await service.EndAsync("testId1", new DateTime(2020, 12, 8, 19, 30, 30));

            Assert.Equal(new DateTime(2020, 12, 8, 19, 30, 30), repository.All().First().EndTime);
            Assert.True(repository.All().First().Status == Status.Finished);
        }

        [Fact]
        public async Task DeclineShouldSetIsDeletedToTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);

            var service = new AppointmentsService(repository);

            await service.DeclineAsync("testId1");

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task CancelShouldSetIsDeletedToTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);

            var service = new AppointmentsService(repository);

            await service.CancelAsync("testId1");

            Assert.Equal(1, repository.All().Count());
        }

        [Fact]
        public async Task StartAsyncShouldSetStatusToInPogress()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);

            var service = new AppointmentsService(repository);

            await service.StartAsync("testId1");

            Assert.Equal(Status.InProgress, repository.All().First().Status);
        }

        [Fact]
        public async Task GetAppointmentsInProgressShouldReturnCorrectCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);

            var service = new AppointmentsService(repository);

            await service.StartAsync("testId1");
            var actualCount = service.GetAppointmentsInProgressCount("testVetId123");

            Assert.Equal(1, actualCount);
        }

        [Fact]
        public async Task GetAppointmentInProgressShouldReturnCorrectEntity()
        {
            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);

            var service = new AppointmentsService(repository);

            await service.StartAsync("testId1");
            var appointment = service.GetAppointmentInProgress<AppointmentViewModelTest>("testVetId123");

            Assert.Equal("testPetId123", appointment.PetId);
            Assert.Equal("testSubject1", appointment.Subject);
            Assert.Equal("testVetId123", appointment.VetId);
        }

        [Fact]
        public async Task GetByIdShouldReturnCorrectEntity()
        {
            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);

            var service = new AppointmentsService(repository);

            var appointment = service.GetById<AppointmentViewModelTest>("testId1");

            Assert.Equal("testPetId123", appointment.PetId);
            Assert.Equal("testSubject1", appointment.Subject);
            Assert.Equal("testVetId123", appointment.VetId);
            Assert.Equal("testOwnerId123", appointment.OwnerId);
        }

        private static async Task PopulateRepositoryWithData(EfDeletableEntityRepository<Appointment> repository)
        {
            await repository.AddAsync(new Appointment
            {
                Id = "testId1",
                Date = DateTime.UtcNow,
                PetId = "testPetId123",
                Subject = "testSubject1",
                VetId = "testVetId123",
                OwnerId = "testOwnerId123",
            });
            await repository.AddAsync(
                new Appointment
                {
                    Id = "testId2",
                    Date = DateTime.UtcNow,
                    PetId = "testPetId123",
                    Subject = "testSubject2",
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                });

            await repository.SaveChangesAsync();
        }

        [Fact]
        public void GetVetUpcomingAppointmentsShouldReturnCorrectEntities()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.All())
            .Returns(this.GetTestDataForUpcomingAppointments().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            IAppointmentsService service = new AppointmentsService(repository.Object);

            List<AppointmentViewModelTest> upcomingAppointments = service.GetVetUpcomingAppointments<AppointmentViewModelTest>("testVetId123").ToList();

            for (int i = 1; i <= upcomingAppointments.Count(); i++)
            {
                Assert.Equal("testSubject" + i, upcomingAppointments[i - 1].Subject);
                Assert.Equal("testPetId123" + i, upcomingAppointments[i - 1].PetId);
                Assert.Equal("testVetId123", upcomingAppointments[i - 1].VetId);
                Assert.Equal("testOwnerId123", upcomingAppointments[i - 1].OwnerId);
            }
        }

        [Fact]
        public void GetVetUpcomingAppointmentsShouldReturnCorrectCount()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.All())
            .Returns(this.GetTestDataForUpcomingAppointments().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            IAppointmentsService service = new AppointmentsService(repository.Object);

            List<AppointmentViewModelTest> upcomingAppointments = service.GetVetUpcomingAppointments<AppointmentViewModelTest>("AnotherVetId123").ToList();

            Assert.Single(upcomingAppointments);
        }

        [Fact]
        public void GetOwnerUpcomingAppointmentsShouldReturnCorrectCount()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForUpcomingAppointments().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            IAppointmentsService service = new AppointmentsService(repository.Object);

            List<AppointmentViewModelTest> upcomingAppointments = service.GetOwnerUpcomingAppointments<AppointmentViewModelTest>("testOwnerId123").ToList();

            Assert.Equal(2, upcomingAppointments.Count());
        }

        [Fact]
        public void GetOwnerUpcomingAppointmentsShouldReturnCorrectEntities()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.AllAsNoTracking())
            .Returns(this.GetTestDataForUpcomingAppointments().AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            IAppointmentsService service = new AppointmentsService(repository.Object);

            List<AppointmentViewModelTest> upcomingAppointments = service.GetOwnerUpcomingAppointments<AppointmentViewModelTest>("testAnotherOwnerId123").ToList();

            for (int i = 1; i <= upcomingAppointments.Count; i++)
            {
                Assert.Equal(new DateTime(2020, 12, 13, 14, 30, 30), upcomingAppointments[i - 1].StartTime);
                Assert.Equal("testPetId1233", upcomingAppointments[i - 1].PetId);
                Assert.Equal("testSubject3", upcomingAppointments[i - 1].Subject);
                Assert.Equal("AnotherVetId123", upcomingAppointments[i - 1].VetId);
                Assert.Equal("testAnotherOwnerId123", upcomingAppointments[i - 1].OwnerId);
            }
        }

        [Fact]
        public void GetVetPastAppointmentsShouldReturnCorrectCount()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.All())
            .Returns(new List<Appointment>
            {
                new Appointment
                {
                PetId = "testPetId123",
                Subject = "testSubject1",
                VetId = "testVetId123",
                OwnerId = "testOwnerId123",
                Status = Status.Finished,
                },
            }.AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            IAppointmentsService service = new AppointmentsService(repository.Object);

            List<AppointmentViewModelTest> pastAppointments = service.GetVetPastAppointments<AppointmentViewModelTest>("testVetId123").ToList();

            Assert.Single(pastAppointments);
        }

        [Fact]
        public void GetVetPastAppointmentsShouldReturnCorrectEntities()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.All())
            .Returns(new List<Appointment>
            {
                new Appointment
                {
                PetId = "testPetId123",
                Subject = "testSubject1",
                VetId = "testVetId123",
                OwnerId = "testOwnerId123",
                Status = Status.Finished,
                },
            }.AsQueryable());

            AutoMapperConfig.RegisterMappings(typeof(AppointmentViewModelTest).Assembly);

            IAppointmentsService service = new AppointmentsService(repository.Object);

            List<AppointmentViewModelTest> pastAppointments = service.GetVetPastAppointments<AppointmentViewModelTest>("testVetId123").ToList();

            for (int i = 1; i <= pastAppointments.Count; i++)
            {
                Assert.Equal("testPetId123", pastAppointments[i - 1].PetId);
                Assert.Equal("testSubject1", pastAppointments[i - 1].Subject);
                Assert.Equal("testVetId123", pastAppointments[i - 1].VetId);
                Assert.Equal("testOwnerId123", pastAppointments[i - 1].OwnerId);
            }
        }

        private List<Appointment> GetTestData()
        {
            return new List<Appointment>()
            {
                new Appointment
                {
                    StartTime = new DateTime(2020, 12, 13, 14, 30, 30),
                    PetId = "testPetId123",
                    Subject = "testSubject1",
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                },
                new Appointment
                {
                     StartTime = new DateTime(2020, 12, 13, 14, 30, 30),
                     PetId = "testPetId123",
                     Subject = "testSubject2",
                     VetId = "testVetId123",
                     OwnerId = "testOwnerId123",
                },
                new Appointment
                {
                     StartTime = new DateTime(2020, 12, 13, 14, 30, 30),
                     PetId = "testPetId123",
                     Subject = "testSubject3",
                     VetId = "testVetId123",
                     OwnerId = "testOwnerId123",
                },
            };
        }

        private List<Appointment> GetTestDataForUpcomingAppointments()
        {
            return new List<Appointment>()
            {
                new Appointment
                {
                    StartTime = new DateTime(2020, 12, 13, 14, 30, 30),
                    PetId = "testPetId1231",
                    Subject = "testSubject1",
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                    IsAcceptedByVet = true,
                    IsCancelledByOwner = false,
                    Status = Status.Upcoming,
                },
                new Appointment
                {
                     StartTime = new DateTime(2020, 12, 13, 14, 30, 30),
                     PetId = "testPetId1232",
                     Subject = "testSubject2",
                     VetId = "testVetId123",
                     OwnerId = "testOwnerId123",
                     IsAcceptedByVet = true,
                     IsCancelledByOwner = false,
                     Status = Status.Upcoming,
                },
                new Appointment
                {
                     StartTime = new DateTime(2020, 12, 13, 14, 30, 30),
                     PetId = "testPetId1233",
                     Subject = "testSubject3",
                     VetId = "AnotherVetId123",
                     OwnerId = "testAnotherOwnerId123",
                     IsAcceptedByVet = true,
                     IsCancelledByOwner = false,
                     Status = Status.Upcoming,
                },
                new Appointment
                {
                     StartTime = new DateTime(2020, 12, 13, 14, 30, 30),
                     PetId = "testPetId123",
                     Subject = "testSubject4",
                     VetId = "testVetId123",
                     OwnerId = "testOwnerId123",
                     Status = Status.Finished,
                },
            };
        }
    }
}
