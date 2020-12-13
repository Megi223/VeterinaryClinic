namespace VeterinaryClinic.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using VeterinaryClinic.Data;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Repositories;
    using VeterinaryClinic.Web.ViewModels.Appointments;
    using Xunit;

    public class PetsMedicationsServiceTests
    {
        [Fact]
        public async Task PrescribeMedicationAsyncShouldAddToBothRepositories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var medicationsRepository = new EfDeletableEntityRepository<Medication>(new ApplicationDbContext(options.Options));
            var petsMedicationsRepository = new EfDeletableEntityRepository<PetsMedications>(new ApplicationDbContext(options.Options));

            var service = new PetsMedicationsService(petsMedicationsRepository, medicationsRepository);

            await service.PrescribeMedicationAsync(new PrescribeMedicationViewModel { PetId = "testPetId123", Medications = new List<PetsMedicationsInputModel> { new PetsMedicationsInputModel { MedicationName = "testMedicationName", MedicationNumberOfDosesPerServing = "5" }, new PetsMedicationsInputModel { MedicationName = "testMedicationName2", MedicationNumberOfDosesPerServing = "3" } } });

            var medicationsRepositoryActualCount = medicationsRepository.All().Count();
            var petsMedicationRepositoryActualCount = medicationsRepository.All().Count();

            Assert.Equal(2, medicationsRepositoryActualCount);
            Assert.Equal(2, petsMedicationRepositoryActualCount);
        }

        [Fact]
        public async Task EndMedicationAsyncShouldDeleteFromBothRepositories()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString());
            var medicationsRepository = new EfDeletableEntityRepository<Medication>(new ApplicationDbContext(options.Options));
            var petsMedicationsRepository = new EfDeletableEntityRepository<PetsMedications>(new ApplicationDbContext(options.Options));

            var service = new PetsMedicationsService(petsMedicationsRepository, medicationsRepository);

            await medicationsRepository.AddAsync(new Medication { Id = 1, Name = "testMedicationName", NumberOfDosesPerServing = "5" });
            await medicationsRepository.AddAsync(new Medication { Id = 2, Name = "testMedicationName2", NumberOfDosesPerServing = "3" });
            await medicationsRepository.SaveChangesAsync();

            await petsMedicationsRepository.AddAsync(new PetsMedications { PetId = "testPetId123", MedicationId = 1 });
            await petsMedicationsRepository.AddAsync(new PetsMedications { PetId = "testPetId123", MedicationId = 2 });
            await petsMedicationsRepository.SaveChangesAsync();

            await service.EndMedicationAsync(1);

            var medicationsRepositoryActualCount = medicationsRepository.All().Count();
            var petsMedicationRepositoryActualCount = medicationsRepository.All().Count();

            Assert.Equal(1, medicationsRepositoryActualCount);
            Assert.Equal(1, petsMedicationRepositoryActualCount);
        }
    }
}
