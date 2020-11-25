using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Services;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Pets;

namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class PetController : Controller
    {
        private readonly IPetsService petsService;
        
        public PetController(IPetsService petsService)
        {
            this.petsService = petsService;
        }

        public IActionResult Details(string id)
        {
            var model = this.petsService.GetById<PetViewModel>(id);
            return this.View(model);
        }
    }
}
