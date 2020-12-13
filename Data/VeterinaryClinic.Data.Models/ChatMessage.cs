namespace VeterinaryClinic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;
    using VeterinaryClinic.Data.Models.Enumerations;

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
