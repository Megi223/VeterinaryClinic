using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    public class PetsMedicationsInputModel 
    {


        [Required]
        public string MedicationName { get; set; }
        [Required]
        public string MedicationNumberOfDosesPerServing { get; set; }
    }
}
