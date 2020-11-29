using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Web.ViewModels.Appointments;

namespace VeterinaryClinic.Services.Data
{
    public interface IAppointmentsService
    {
        Task CreateAppointmentAsync(RequestAppointmentViewModel model, string ownerId);
    }
}
