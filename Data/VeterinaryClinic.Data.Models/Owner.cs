namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class Owner : BaseDeletableModel<string>
    {
        public Owner()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Pets = new HashSet<Pet>();
            this.Comments = new HashSet<Comment>();
            this.Reminders = new HashSet<Reminder>();
            this.ChatNotifications = new HashSet<ChatNotification>();
            this.Rating = new HashSet<Rating>();
            this.Appointments = new HashSet<Appointment>();
            this.Reviews = new HashSet<Review>();
        }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string LastName { get; set; }

        public string City { get; set; }

        public string ProfilePicture { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Pet> Pets { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<ChatNotification> ChatNotifications { get; set; }

        public virtual ICollection<Reminder> Reminders { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }

        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}
