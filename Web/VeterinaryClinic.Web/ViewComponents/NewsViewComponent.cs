namespace VeterinaryClinic.Web.ViewComponents
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.News;

    public class NewsViewComponent : ViewComponent
    {
        private readonly INewsService newsService;

        public NewsViewComponent(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new NewsBarViewModel
            {
                LatestNews = this.newsService.GetLatestNews<NewsViewModel>(),
            };

            return this.View(model);
        }
    }
}
