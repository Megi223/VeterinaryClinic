using System;
using System.Collections.Generic;
using System.Text;

namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    public class PastAppointmentViewModel : AppointmentBaseViewModel
    {
        public DateTime EndTime { get; set; }

        public double Duration => (this.EndTime - this.StartTime).TotalMinutes;

        public string DurationFormatted => this.Duration.ToString("0");
    }
}
