namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class Doctor : BaseDeletableModel<string>
    {
        public Doctor()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ChatNotifications = new HashSet<ChatNotification>();
            this.Reminders = new HashSet<Reminder>();
        }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string LastName { get; set; }

        public float Rating { get; set; }

        public string ProfilePicture { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ChatNotification> ChatNotifications { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }
    }
}
