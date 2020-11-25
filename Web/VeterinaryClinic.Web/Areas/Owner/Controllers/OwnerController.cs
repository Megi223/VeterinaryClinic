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
using VeterinaryClinic.Services;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Pets;

namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class OwnerController : Controller
    {
        private readonly IPetsService petsService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IVetsService vetsService;
        private readonly IOwnersService ownersService;
        private readonly ICloudinaryService cloudinaryService;

        public OwnerController(IPetsService petsService, IVetsService vetsService,ICloudinaryService cloudinaryService,UserManager<ApplicationUser> userManager, IOwnersService ownersService)
        {
            this.petsService = petsService;
            this.vetsService = vetsService;
            this.cloudinaryService = cloudinaryService;
            this.userManager = userManager;
            this.ownersService = ownersService;
        }

        public IActionResult MyPets()
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var pets = this.petsService.GetPets<AllPetsViewModel>();
            return this.View();
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
