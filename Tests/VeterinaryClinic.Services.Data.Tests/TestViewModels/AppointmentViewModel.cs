using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    public class AppointmentViewModel : IMapFrom<Appointment>
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
