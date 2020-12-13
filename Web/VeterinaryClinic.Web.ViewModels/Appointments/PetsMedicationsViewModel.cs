namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class PetsMedicationsViewModel : IMapFrom<PetsMedications>
    {
        public int Id { get; set; }

        public string MedicationName { get; set; }

        public string MedicationNumberOfDosesPerServing { get; set; }
    }
}
