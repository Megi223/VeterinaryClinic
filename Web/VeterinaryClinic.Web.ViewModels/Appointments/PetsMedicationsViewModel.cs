using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    public class PetsMedicationsViewModel : IMapFrom<PetsMedications>
    {
        public int Id { get; set; }

        public string MedicationName { get; set; }

        public string MedicationNumberOfDosesPerServing { get; set; }
    }
}
