namespace VeterinaryClinic.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services;
    using VeterinaryClinic.Web.Controllers;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly INewsScraperService newsScraperService;

        public AdministrationController(INewsScraperService newsScraperService)
        {
            this.newsScraperService = newsScraperService;
        }

        // public IActionResult Register()
        // {
        //    return this.View();
        // }
        //
        // public IActionResult Login()
        // {
        //    return this.View();
        // }
    }
}
