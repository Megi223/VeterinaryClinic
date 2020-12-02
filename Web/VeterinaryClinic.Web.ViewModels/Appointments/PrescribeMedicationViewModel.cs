using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    public class PrescribeMedicationViewModel
    {
        public IEnumerable<PetsMedicationsInputModel> Medications { get; set; }

        public string PetId { get; set; }
    }
}
