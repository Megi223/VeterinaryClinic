using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Common.Models;
using VeterinaryClinic.Data.Models.Enumerations;

namespace VeterinaryClinic.Data.Models
{
    public class ChatMessage : BaseDeletableModel<int>
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public bool IsRead { get; set; }

        public RoleName SenderRole { get; set; }

        public RoleName ReceiverRole { get; set; }

    }
}
