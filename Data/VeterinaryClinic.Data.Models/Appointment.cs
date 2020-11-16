namespace VeterinaryClinic.Data.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;
    using VeterinaryClinic.Data.Models.Enumerations;

    public class Appointment : BaseDeletableModel<string>
    {
        public Appointment()
        {
            this.Id = Guid.NewGuid().ToString();
            this.IsCancelled = false;
        }

        public Status Status { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        [Required]
        public string PetId { get; set; }

        public virtual Pet Pet { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public bool IsCancelled { get; set; }
    }
}
