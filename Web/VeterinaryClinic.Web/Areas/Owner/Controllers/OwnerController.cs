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
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IVetsService vetsService;
        private readonly IOwnersService ownersService;
        private readonly ICloudinaryService cloudinaryService;
        private readonly IPaginatedMetaService paginatedMetaService;


        public OwnerController(IPetsService petsService, IVetsService vetsService,ICloudinaryService cloudinaryService,UserManager<ApplicationUser> userManager, IOwnersService ownersService,IPaginatedMetaService paginatedMetaService)
        {
            this.petsService = petsService;
            this.vetsService = vetsService;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
            this.ownersService = ownersService;
            this.paginatedMetaService = paginatedMetaService;
        }

        public IActionResult MyPets(int id = 1)
        {
            var petsCount = this.petsService.GetCount();
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
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(userId);
            var pets = this.petsService.GetAllForAPage<AllPetsViewModel>(id, ownerId);
            return this.View(pets);
        }

        public IActionResult AddPet()
        {
            var vets = this.vetsService.GetAll<VetDropDown>();
            var model = new AddPetInputModel();
            model.Vet = vets;
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPet(AddPetInputModel model)
        {
            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(currentUserId);
            string photoUrl = string.Empty;
            if (model.Picture != null)
            {
                photoUrl = await this.cloudinaryService.UploudAsync(model.Picture);
            }
            await this.petsService.AddPetAsync(ownerId, model, photoUrl);
            return this.RedirectToAction("MyPets");
        }
    }
}
