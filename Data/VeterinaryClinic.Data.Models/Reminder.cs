namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class Reminder : BaseDeletableModel<string>
    {
        public Reminder()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(GlobalConstants.ReminderContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }
    }
}
