using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    public class CancelAppointmentViewModel : AppointmentBaseViewModel, IMapFrom<Appointment>
    {
        public string VetId { get; set; }

        public string VetFirstName { get; set; }

        public string VetLastName { get; set; }

        public string VetFullName => this.VetFirstName + " " + this.VetLastName;
    }
}
