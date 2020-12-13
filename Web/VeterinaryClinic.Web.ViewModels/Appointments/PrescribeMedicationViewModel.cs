namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using System.Collections.Generic;

    public class PrescribeMedicationViewModel
    {
        public IEnumerable<PetsMedicationsInputModel> Medications { get; set; }

        public string PetId { get; set; }
    }
}
