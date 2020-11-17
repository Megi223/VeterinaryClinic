using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Data.Models;


namespace VeterinaryClinic.Web.ViewModels.News
{
    public class NewsBarViewModel
    {
        public IEnumerable<NewsViewModel> LatestNews { get; set; }
    }
}
