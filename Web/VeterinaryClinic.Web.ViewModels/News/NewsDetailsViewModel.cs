namespace VeterinaryClinic.Web.ViewModels.News
{
    using System;

    using VeterinaryClinic.Services.Mapping;

    public class NewsDetailsViewModel : IMapFrom<VeterinaryClinic.Data.Models.News>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Content { get; set; }
    }
}
