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

        public DateTime ReceivedOn { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        [Required]
        public string DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public bool IsCompleted { get; set; }

        public int MedicationId { get; set; }

        public virtual Medication Medication { get; set; }

        public DateTime DateTime { get; set; }
    }
}
