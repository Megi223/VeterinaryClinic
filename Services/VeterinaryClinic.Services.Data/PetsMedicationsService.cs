using System;
using System.Collections.Generic;
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

        public async Task PrescribeMedication(PrescribeMedicationViewModel model)
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
                PetsMedications petsMedications = new PetsMedications
                {
                    PetId = model.PetId,
                    Medication = medication,
                };
                await this.petsMedicationsRepository.AddAsync(petsMedications);
                await this.petsMedicationsRepository.SaveChangesAsync();
            }
        }
    }
}
