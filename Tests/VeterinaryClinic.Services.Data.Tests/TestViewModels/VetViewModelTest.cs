namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    using System;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class VetViewModelTest : IMapFrom<Vet>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        public string Specialization { get; set; }

        public DateTime HireDate { get; set; }
    }
}
