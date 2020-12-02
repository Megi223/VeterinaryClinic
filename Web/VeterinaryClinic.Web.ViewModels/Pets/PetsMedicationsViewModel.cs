using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Pets
{
    public class PetsMedicationsViewModel : IMapFrom<PetsMedications>
    {
        public string MedicationName { get; set; }

        public string MedicationNumberOfDosesPerServing { get; set; }

        
    }
}
