using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Services.Data;

namespace VeterinaryClinic.Web.Controllers
{
    public class SearchController : Controller
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public async Task<IActionResult> Index(string search, bool veterinarians, bool services, bool news)
        {
            if (search != null)
            {
                //var vetsFound = new List<string>();
                //var servicesFound = new List<string>();
                //var newsFound = new List<string>();
                //if (veterinarians == true)
                //{
                //    vetsFound = this.searchService.SearchVet(search);
                //}
                //if (services == true)
                //{
                //    servicesFound = this.searchService.SearchServices(search);
                //}
                //if (news == true)
                //{
                //    newsFound = this.searchService.SearchNews(search);
                //}
                //return this.View(newsFound);
                var newsFound = new List<string>();
                newsFound = this.searchService.SearchNews(search);
                return this.View(newsFound);

            }
            else
            {
                return View(this.searchService.SearchNews(""));
            }
                

            
        }
    }
}
