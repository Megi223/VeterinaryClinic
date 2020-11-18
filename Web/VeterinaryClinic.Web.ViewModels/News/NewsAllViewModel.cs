using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Services.Mapping;


namespace VeterinaryClinic.Web.ViewModels.News
{
    public class NewsAllViewModel : IMapFrom<VeterinaryClinic.Data.Models.News>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Summary { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
