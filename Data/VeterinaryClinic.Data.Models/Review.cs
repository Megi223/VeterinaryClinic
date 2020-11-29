namespace VeterinaryClinic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        [Required]
        public string Content { get; set; }

        [Required]
        public string OwnerId { get; set; }

        public Owner Owner { get; set; }
    }
}
