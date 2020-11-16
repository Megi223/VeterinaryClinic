namespace VeterinaryClinic.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class ChatNotification : BaseDeletableModel<string>
    {
        public ChatNotification()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(GlobalConstants.ChatNotificationContentMaxLength)]
        public string Content { get; set; }

        public DateTime ReceivedOn { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public bool IsRead { get; set; }
    }
}
