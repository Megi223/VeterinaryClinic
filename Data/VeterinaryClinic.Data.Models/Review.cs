namespace VeterinaryClinic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;

    public class Review : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(100)]
        public string Content { get; set; }
    }
}
