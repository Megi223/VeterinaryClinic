using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Common.Repositories;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Web.ViewModels.News;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewComponents
{
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
