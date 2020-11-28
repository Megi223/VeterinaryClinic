namespace VeterinaryClinic.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Pioneer.Pagination;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.News;

    public class NewsController : Controller
    {
        private readonly INewsScraperService newsScraperService;
        private readonly INewsService newsService;
        private readonly IPaginatedMetaService paginatedMetaService;

        public NewsController(INewsScraperService newsScraperService, INewsService newsService, IPaginatedMetaService paginatedMetaService)
        {
            this.newsScraperService = newsScraperService;
            this.newsService = newsService;
            this.paginatedMetaService = paginatedMetaService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public async Task<IActionResult> Add()
        {
            await this.newsScraperService.PopulateDbWithNews();
            return this.RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> All(int id = 1)
        {
            var newsCount = this.newsService.GetCount();
            var newsOnPage = GlobalConstants.NewsOnOnePage;
            var allPagesCount = (newsCount / newsOnPage) + 1;
            if (id < 1)
            {
                return this.RedirectToAction("All", new { id = 1 });
            }
            else if (id > allPagesCount)
            {
                return this.RedirectToAction("All", new { id = allPagesCount });
            }

            var viewModel = this.newsService.GetAllForAPage<NewsAllViewModel>(id);
            this.ViewBag.PaginatedMeta = this.paginatedMetaService.GetMetaData(newsCount, id, newsOnPage);
            return this.View(viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            var newsCount = this.newsService.GetCount();
            if (id < 1 || id > newsCount)
            {
                return this.RedirectToAction("StatusCodeError", "Home", new { errorCode = 404 });
            }

            var news = this.newsService.GetById<NewsDetailsViewModel>(id);
            return this.View(news);
        }
    }
}
