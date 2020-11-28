namespace VeterinaryClinic.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Search;

    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public async Task<IActionResult> Index(string search, bool veterinarians, bool services, bool news)
        {
            var vetsFound = new List<VetsFoundViewModel>();
            var servicesFound = new List<ServicesFoundViewModel>();
            var newsFound = new List<NewsFoundViewModel>();
            if (veterinarians == true)
            {
                vetsFound = this.searchService.SearchVet<VetsFoundViewModel>(search);
            }

            if (services == true)
            {
                servicesFound = this.searchService.SearchServices<ServicesFoundViewModel>(search);
            }

            if (news == true)
            {
                newsFound = this.searchService.SearchNews<NewsFoundViewModel>(search);
            }

            SearchResultsViewModel model = new SearchResultsViewModel
            {
                Vets = vetsFound,
                Services = servicesFound,
                News = newsFound,
            };

            return this.View(model);
        }
    }
}
