namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    using System;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Services.Mapping;

    public class PetViewModelTest : IMapFrom<Pet>
    {
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public string VetId { get; set; }

        public string IdentificationNumber { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public TypeOfAnimal Type { get; set; }

        public bool Sterilised { get; set; }

        public float Weight { get; set; }
    }
}
