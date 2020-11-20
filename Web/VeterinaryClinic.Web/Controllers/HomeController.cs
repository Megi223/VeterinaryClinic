namespace VeterinaryClinic.Web.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IServiceScraperService serviceScraperService;

        public HomeController(IServiceScraperService serviceScraperService,IGalleryService galleryService)
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

        public IActionResult Contact()
        {
            return this.View();
        }

        //[HttpPost]
        //public IActionResult Contact()
        //{
        //    //this method should receive an input model and then continue with /business /logic
        //    return this.View();
        //}

        //TODO: move it later
        public async Task<IActionResult> AddServices()
        {
            await this.serviceScraperService.PopulateDbWithServices();
            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> StatusCodeError(int errorCode)
        {
            if (errorCode == 404)
            {
                return this.View("NotFound");
            }
            return this.RedirectToAction("Error", "Home");
        }

    }
}
