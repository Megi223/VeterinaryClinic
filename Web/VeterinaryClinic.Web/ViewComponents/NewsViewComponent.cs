namespace VeterinaryClinic.Web.ViewComponents
{
    using System.Linq;

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Data.Common.Repositories;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;
    using VeterinaryClinic.Web.ViewModels.News;

    public class NewsViewComponent : ViewComponent
    {
        private readonly IDeletableEntityRepository<News> newsRepository;

        public NewsViewComponent(IDeletableEntityRepository<News> newsRepository)
        {
            this.newsRepository = newsRepository;
        }

        public IViewComponentResult Invoke()
        {
            var model = new NewsBarViewModel
            {
                LatestNews = this.newsRepository.All().OrderByDescending(x => x.CreatedOn).To<NewsViewModel>().Take(2).ToList(),
            };

            return this.View(model);
        }
    }
}
