namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using System.ComponentModel.DataAnnotations;

    public class PetsMedicationsInputModel
    {
        [Required]
        public string MedicationName { get; set; }

        [Required]
        public string MedicationNumberOfDosesPerServing { get; set; }
    }
}
