namespace VeterinaryClinic.Web.ViewComponents
{

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Services.Data;
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
