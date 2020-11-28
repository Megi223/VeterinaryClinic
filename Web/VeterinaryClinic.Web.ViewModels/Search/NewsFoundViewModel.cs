namespace VeterinaryClinic.Web.ViewModels.Search
{
    using System;

    using VeterinaryClinic.Services.Mapping;

    public class NewsFoundViewModel : IMapFrom<VeterinaryClinic.Data.Models.News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Summary { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
