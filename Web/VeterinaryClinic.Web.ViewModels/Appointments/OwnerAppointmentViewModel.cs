﻿using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    public class OwnerAppointmentViewModel : IMapFrom<Appointment>
    {
        public string Id { get; set; }

        public string VetFirstName { get; set; }

        public string VetLastName { get; set; }

        public string VetFullName => this.VetFirstName + " " + this.VetLastName;

        public string PetName { get; set; }

        public string PetId { get; set; }

        public string Subject { get; set; }

        public DateTime StartTime { get; set; }
    }
}
