namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using System;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class PendingAppointmentViewModel : IMapFrom<Appointment>
    {
        public string Id { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerFullName => this.OwnerFirstName + " " + this.OwnerLastName;

        public string PetName { get; set; }

        public string PetId { get; set; }

        public string Subject { get; set; }

        public DateTime StartTime { get; set; }
    }
}
