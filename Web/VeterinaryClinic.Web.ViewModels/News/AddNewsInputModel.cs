using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.News
{
    public class AddNewsInputModel : IMapFrom<VeterinaryClinic.Data.Models.News>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Summary { get; set; }

        [Required]
        public string Content { get; set; }

        public IFormFile Image { get; set; }
    }
}
