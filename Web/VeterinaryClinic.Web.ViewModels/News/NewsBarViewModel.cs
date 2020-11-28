namespace VeterinaryClinic.Web.ViewModels.News
{
    using System.Collections.Generic;

    public class NewsBarViewModel
    {
        public IEnumerable<NewsViewModel> LatestNews { get; set; }
    }
}
