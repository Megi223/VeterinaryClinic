using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;
using VeterinaryClinic.Data.Repositories;
using VeterinaryClinic.Services.Data.Tests.TestViewModels;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Web.ViewModels.Appointments;
using Xunit;

namespace VeterinaryClinic.Services.Data.Tests
{
    public class AppointmentsServiceTests
    {
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

        // TODO
        [Fact]
        public async Task GetVetPendingAppointmentsShouldReturnCorrectAppointments()
        {
            var repository = new Mock<IDeletableEntityRepository<Appointment>>();

            repository.Setup(r => r.AllAsNoTracking())

            .Returns(this.GetTestDataWithIds().AsQueryable());

            //AutoMapperConfig.RegisterMappings(Assembly.GetCallingAssembly());

            AutoMapperConfig.RegisterMappings(typeof(IEnumerable<AppointmentInProgressViewModel>).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(AppointmentInProgressViewModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(AppointmentInProgressViewModel).Assembly);

            IAppointmentsService service = new AppointmentsService(repository.Object);
           
            List<AppointmentInProgressViewModel> pendingAppointments = service.GetVetPendingAppointments<AppointmentInProgressViewModel>("testVetId123").ToList();
            Assert.Equal(3, pendingAppointments.Count());
            /*for (int i = 1; i <= gallery.Count(); i++)
            {
                Assert.Equal("test" + i, gallery[i - 1].Title);
                Assert.Null(gallery[i - 1].ImageUrl);
            }*/

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

            await service.EndAsync("testId1",new DateTime(2020,12,8,19,30,30));

            Assert.Equal(new DateTime(2020, 12, 8, 19, 30, 30), repository.All().First().EndTime);
            Assert.True(repository.All().First().Status==Status.Finished);
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

            Assert.Equal(1,repository.All().Count());
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
            var actualCount=service.GetAppointmentsInProgressCount("testVetId123");

            Assert.Equal(1, actualCount);
        }

        // TODO
        [Fact]
        public async Task GetAppointmentsShouldReturnCorrectEntities()
        {
            AutoMapperConfig.RegisterMappings(typeof(AppointmentInProgressViewModel).Assembly);
            AutoMapperConfig.RegisterMappings(typeof(Appointment).Assembly);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());

            var repository = new EfDeletableEntityRepository<Appointment>(new ApplicationDbContext(options.Options));

            await PopulateRepositoryWithData(repository);
            
            var service = new AppointmentsService(repository);

            await service.StartAsync("testId1");
            var appointment = service.GetAppointmentInProgress<AppointmentInProgressViewModel>("testVetId123");

            Assert.Equal("testPetId123", appointment.PetId);
            Assert.Equal("testSubject1", appointment.Subject);
            Assert.Equal("testVetId123", appointment.VetId);
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

        private List<Appointment> GetTestData()
        {
            return new List<Appointment>()
            {
                new Appointment
                {
                    Date = DateTime.UtcNow, 
                    PetId = "testPetId123", 
                    Subject = "testSubject1", 
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                },
                new Appointment
                {
                    Date = DateTime.UtcNow,
                    PetId = "testPetId123",
                    Subject = "testSubject2",
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                },
                new Appointment
                {
                    Date = DateTime.UtcNow,
                    PetId = "testPetId123",
                    Subject = "testSubject3",
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                },
            };
        }

        private List<Appointment> GetTestDataWithIds()
        {
            return new List<Appointment>()
            {
                new Appointment
                {
                    Id="testId1",
                    Date = DateTime.UtcNow,
                    PetId = "testPetId123",
                    Subject = "testSubject1",
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                },
                new Appointment
                {
                    Id="testId2",
                    Date = DateTime.UtcNow,
                    PetId = "testPetId123",
                    Subject = "testSubject2",
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                },
                new Appointment
                {
                    Id="testId3",
                    Date = DateTime.UtcNow,
                    PetId = "testPetId123",
                    Subject = "testSubject3",
                    VetId = "testVetId123",
                    OwnerId = "testOwnerId123",
                },
            };
        }


    }
}
