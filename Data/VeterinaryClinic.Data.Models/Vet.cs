﻿namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class Vet : BaseDeletableModel<string>
    {
        public Vet()
        {
            this.Id = Guid.NewGuid().ToString();
            this.ChatMessages = new HashSet<ChatMessage>();
            this.Notifications = new HashSet<Notification>();
            this.VetsServices = new HashSet<VetsServices>();
            this.Comments = new HashSet<Comment>();
            this.Ratings = new HashSet<Rating>();
        }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        [Required]
        [MaxLength(GlobalConstants.SpecializationMaxLength)]
        public string Specialization { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<ChatMessage> ChatMessages { get; set; }

        public virtual ICollection<Notification> Notifications { get; set; }

        public virtual ICollection<VetsServices> VetsServices { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
