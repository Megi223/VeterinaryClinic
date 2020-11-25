namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;
    using VeterinaryClinic.Data.Models.Enumerations;

    public class Pet : BaseDeletableModel<string>
    {
        public Pet()
        {
            this.Id = Guid.NewGuid().ToString();
            this.PetsMedications = new HashSet<PetsMedications>();
        }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string Name { get; set; }

        public TypeOfAnimal Type { get; set; }

        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public float Weight { get; set; }

        public string Picture { get; set; }

        public int? DiagnoseId { get; set; }

        public virtual Diagnose Diagnose { get; set; }

        [Required]
        public string IdentificationNumber { get; set; }

        public DateTime? Birthday { get; set; }

        [Required]
        public bool Sterilised { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public virtual ICollection<PetsMedications> PetsMedications { get; set; }
    }
}
