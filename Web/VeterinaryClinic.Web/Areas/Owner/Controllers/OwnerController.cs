using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Data;

namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class OwnerController : Controller
    {
        private readonly IPetsService petsService;
        private readonly UserManager<ApplicationUser> userManager;

        public OwnerController(IPetsService petsService)
        {
            this.petsService = petsService;
        }

        public IActionResult MyPets()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var pets = this.petsService.GetPets<AllPetsViewModel>();
            return this.View();
        }
    }
}
