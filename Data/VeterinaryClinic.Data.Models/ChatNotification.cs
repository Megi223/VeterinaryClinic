namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class ChatNotification : BaseDeletableModel<string>
    {
        public ChatNotification()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(DataValidationConstants.ChatNotificationContentMaxLength)]
        public string Content { get; set; }

        public DateTime ReceivedOn { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        [Required]
        public string DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public bool IsRead { get; set; }
    }
}
