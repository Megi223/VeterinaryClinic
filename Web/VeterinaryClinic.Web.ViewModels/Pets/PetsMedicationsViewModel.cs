namespace VeterinaryClinic.Web.ViewModels.Pets
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class PetsMedicationsViewModel : IMapFrom<PetsMedications>
    {
        public string MedicationName { get; set; }

        public string MedicationNumberOfDosesPerServing { get; set; }
    }
}
