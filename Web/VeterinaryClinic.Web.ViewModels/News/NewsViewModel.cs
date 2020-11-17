using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.News
{
    public class NewsViewModel : IMapFrom<VeterinaryClinic.Data.Models.News>
    {
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
