namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class ContactForm : BaseDeletableModel<string>
    {
        public ContactForm()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public Owner Owner { get; set; }

        [Required]
        public string PetId { get; set; }

        public Pet Pet { get; set; }

        [Required]
        public string VetId { get; set; }

        public Vet Vet { get; set; }

        [Required]
        [MaxLength(GlobalConstants.ContactFormContentMaxLength)]
        public string Content { get; set; }

        public bool Accepted { get; set; }
    }
}
