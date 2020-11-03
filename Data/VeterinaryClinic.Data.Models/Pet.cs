namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common;
    using VeterinaryClinic.Data.Common.Models;
    using VeterinaryClinic.Data.Models.Enumerations;

    public class Pet : BaseDeletableModel<string>
    {
        public Pet()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(DataValidationConstants.NameMaxLength)]
        public string Name { get; set; }

        public TypeOfAnimal Type { get; set; }

        [Required]
        public string DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public float Weight { get; set; }

        public string Picture { get; set; }

        public int DiagnoseId { get; set; }

        public virtual Diagnose Diagnose { get; set; }

        [Required]
        public string PassportId { get; set; }

        public DateTime Birthday { get; set; }

        [Required]
        public bool Sterilised { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }
    }
}
