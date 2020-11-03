namespace VeterinaryClinic.Data.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        [Required]
        public string Content { get; set; }

        public DateTime PublishedOn { get; set; }

        [Required]
        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
