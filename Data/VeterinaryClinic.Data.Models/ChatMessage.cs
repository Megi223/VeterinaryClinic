using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Common.Models;

namespace VeterinaryClinic.Data.Models
{
    public class ChatMessage : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.ChatNotificationContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public bool IsRead { get; set; }
    }
}
