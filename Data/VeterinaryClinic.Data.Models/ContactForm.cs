namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common;
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
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(DataValidationConstants.ContactFormContentMaxLength)]
        public string Content { get; set; }

        public bool Answered { get; set; }
    }
}
