namespace VeterinaryClinic.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Services;

    public class ServicesController : Controller
    {
        private readonly IServicesService servicesService;

        public ServicesController(IServicesService servicesService)
        {
            this.servicesService = servicesService;
        }

        public IActionResult All()
        {
            var allServices = this.servicesService.GetAll<ServiceViewModel>();
            return this.View(allServices);
        }

        public IActionResult Details(int id)
        {
            var viewModel = this.servicesService.GetById<ServiceDetailsViewModel>(id);
            return this.View(viewModel);
        }
    }
}
