namespace VeterinaryClinic.Web.Controllers
{
    using System.Diagnostics;

    using VeterinaryClinic.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Services;
    using System.Threading.Tasks;

    public class HomeController : BaseController
    {
        private readonly IServiceScraperService serviceScraperService;

        public HomeController(IServiceScraperService serviceScraperService)
        {
            this.serviceScraperService = serviceScraperService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult FAQ()
        {
            return this.View();
        }

        //TODO: move it later
        public async Task<IActionResult> AddServices()
        {
            await serviceScraperService.PopulateDbWithServices();
            return this.RedirectToAction("Index");
        }
        
    }
}
