namespace VeterinaryClinic.Web.Areas.Administration.Controllers
{
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Services;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly INewsScraperService newsScraperService;

        public AdministrationController(INewsScraperService newsScraperService)
        {
            this.newsScraperService = newsScraperService;
        }

        public IActionResult Register()
        {
            return this.View();
        }

        public IActionResult Login()
        {
            return this.View();
        }

        

    }
}
