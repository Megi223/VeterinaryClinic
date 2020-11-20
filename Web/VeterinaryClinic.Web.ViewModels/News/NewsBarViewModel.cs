namespace VeterinaryClinic.Web.ViewModels.News
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class NewsBarViewModel
    {
        public IEnumerable<NewsViewModel> LatestNews { get; set; }
    }
}
