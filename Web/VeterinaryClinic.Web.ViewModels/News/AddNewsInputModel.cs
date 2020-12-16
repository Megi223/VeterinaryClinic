namespace VeterinaryClinic.Web.ViewModels.News
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using VeterinaryClinic.Services.Mapping;

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
