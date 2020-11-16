namespace VeterinaryClinic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(GlobalConstants.CommentContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }
    }
}
