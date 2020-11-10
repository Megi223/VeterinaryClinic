namespace VeterinaryClinic.Web.Areas.Administration.Controllers
{
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
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
