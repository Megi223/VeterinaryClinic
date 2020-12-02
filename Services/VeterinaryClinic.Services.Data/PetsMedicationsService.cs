using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Web.ViewModels.Appointments;

namespace VeterinaryClinic.Services.Data
{
    public class PetsMedicationsService : IPetsMedicationsService
    {
        private readonly IDeletableEntityRepository<PetsMedications> petsMedicationsRepository;
        private readonly IDeletableEntityRepository<Medication> medicationsRepository;


        public PetsMedicationsService(IDeletableEntityRepository<PetsMedications> petsMedicationsRepository, IDeletableEntityRepository<Medication> medicationsRepository)
        {
            this.petsMedicationsRepository = petsMedicationsRepository;
            this.medicationsRepository = medicationsRepository;
        }

        public async Task EndMedicationAsync(int id)
        {
            var petsMedicationsTarget = this.petsMedicationsRepository.All().Where(x => x.Id == id).FirstOrDefault();
            var medicationIdTarget = this.petsMedicationsRepository.All().Where(x => x.Id == id).FirstOrDefault().MedicationId;
            var medicationTarget = this.medicationsRepository.All().FirstOrDefault(x => x.Id == medicationIdTarget);
            this.petsMedicationsRepository.Delete(petsMedicationsTarget);
            this.medicationsRepository.Delete(medicationTarget);
            await this.petsMedicationsRepository.SaveChangesAsync();
            await this.medicationsRepository.SaveChangesAsync();
        }

        public async Task PrescribeMedicationAsync(PrescribeMedicationViewModel model)
        {
            foreach (var medicationModel in model.Medications)
            {
                Medication medication = new Medication
                {

                    Name = medicationModel.MedicationName,
                    NumberOfDosesPerServing = medicationModel.MedicationNumberOfDosesPerServing,
                };

                await this.medicationsRepository.AddAsync(medication);
                await this.medicationsRepository.SaveChangesAsync();
                ;
                PetsMedications petsMedications = new PetsMedications
                {
                    PetId = model.PetId,
                    MedicationId = medication.Id,
                };
                await this.petsMedicationsRepository.AddAsync(petsMedications);
                await this.petsMedicationsRepository.SaveChangesAsync();
                ;
            }
        }
    }
}
