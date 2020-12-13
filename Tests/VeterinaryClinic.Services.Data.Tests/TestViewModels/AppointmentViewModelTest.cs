using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    public class AppointmentViewModelTest : IMapFrom<Appointment>
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string PetId { get; set; }

        public string Subject { get; set; }

        public DateTime StartTime { get; set; }

        public string VetId { get; set; }
    }
}
