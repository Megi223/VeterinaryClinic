namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using System;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class SendEmailAppointmentViewModel : IMapFrom<Appointment>
    {
        public string VetFirstName { get; set; }

        public string VetLastName { get; set; }

        public string VetFullName => this.VetFirstName + " " + this.VetLastName;

        public DateTime StartTime { get; set; }

        public string OwnerUserEmail { get; set; }
    }
}
