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
    }
}
