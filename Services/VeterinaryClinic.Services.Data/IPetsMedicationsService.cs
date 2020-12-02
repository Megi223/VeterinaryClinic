using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Web.ViewModels.Appointments;

namespace VeterinaryClinic.Services.Data
{
    public interface IPetsMedicationsService
    {
        Task PrescribeMedication(PrescribeMedicationViewModel model);
    }
}
