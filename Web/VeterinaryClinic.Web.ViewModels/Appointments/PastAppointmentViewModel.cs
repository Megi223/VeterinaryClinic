namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using System;

    public class PastAppointmentViewModel : AppointmentBaseViewModel
    {
        public DateTime EndTime { get; set; }

        public DateTime ActualStartTime { get; set; }

        public double Duration => (this.EndTime - this.ActualStartTime).TotalMinutes;

        public string DurationFormatted => this.Duration.ToString("0");
    }
}
