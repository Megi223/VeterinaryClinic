using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pioneer.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Pets;

namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class OwnerController : Controller
    {
        private const int PetsOnOnePage = 3;
        private readonly IPetsService petsService;
        private readonly IOwnersService ownersService;
        private readonly IPaginatedMetaService paginatedMetaService;
        private readonly UserManager<ApplicationUser> userManager;


        public OwnerController(IPetsService petsService, IOwnersService ownersService,IPaginatedMetaService paginatedMetaService, UserManager<ApplicationUser> userManager)
        {
            this.petsService = petsService;
            this.ownersService = ownersService;
            this.paginatedMetaService = paginatedMetaService;
            this.userManager = userManager;
        }

        public IActionResult MyPets(int id = 1)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(userId);
            var petsCount = this.petsService.GetCountForOwner(ownerId);
            var allPagesCount = (petsCount / PetsOnOnePage) + 1;
            if (id < 1)
            {
                return this.RedirectToAction("MyPets", new { id = 1 });
            }
            else if (id > allPagesCount)
            {
                return this.RedirectToAction("MyPets", new { id = allPagesCount });
            }

            this.ViewBag.PaginatedMeta = this.paginatedMetaService.GetMetaData(petsCount, id, PetsOnOnePage);
            var pets = this.petsService.GetAllForAPage<AllPetsViewModel>(id, ownerId);
            return this.View(pets);
        }
    }
}
