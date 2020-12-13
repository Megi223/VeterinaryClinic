namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    using System;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

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
