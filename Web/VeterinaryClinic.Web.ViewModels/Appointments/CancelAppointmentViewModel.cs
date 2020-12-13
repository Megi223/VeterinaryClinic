namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class CancelAppointmentViewModel : AppointmentBaseViewModel, IMapFrom<Appointment>
    {
        public string VetId { get; set; }

        public string VetFirstName { get; set; }

        public string VetLastName { get; set; }

        public string VetFullName => this.VetFirstName + " " + this.VetLastName;
    }
}
