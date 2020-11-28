namespace VeterinaryClinic.Web.ViewModels.Pets
{
    using System;
    using System.Collections.Generic;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Services.Mapping;

    public class PetViewModel : IMapFrom<Pet>
    {
        public string Name { get; set; }

        public TypeOfAnimal Type { get; set; }

        public string TypeAsString => this.Type.ToString();

        public string VetName => this.Vet.FirstName + " " + this.Vet.LastName;

        public Vet Vet { get; set; }

        public float Weight { get; set; }

        public string Picture { get; set; }

        public int? DiagnoseId { get; set; }

        public Diagnose Diagnose { get; set; }

        public string DiagnoseName => this.DiagnoseId != null ? this.Diagnose.Name : null;

        public string DiagnoseDescription => this.DiagnoseId != null ? this.Diagnose.Description : null;

        public string IdentificationNumber { get; set; }

        public DateTime? Birthday { get; set; }

        public string BirthdayAsString => this.Birthday.Value.ToString("d");

        public bool Sterilised { get; set; }

        public string SterilisedAsString => this.Sterilised ? "Yes" : "No";

        public Gender Gender { get; set; }

        public string GenderAsString => this.Gender.ToString();

        public Owner Owner { get; set; }

        public string OwnerName => this.Owner.FirstName + " " + this.Owner.LastName;

        public ICollection<PetsMedications> PetsMedications { get; set; }

        public string PetsMedicationsAsString => string.Join(", ", this.PetsMedications);
    }
}
