namespace VeterinaryClinic.Data.Models
{
    using System;

    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;

    public class News : BaseDeletableModel<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public string Summary { get; set; }

        public string ImageUrl { get; set; }
    }
}
