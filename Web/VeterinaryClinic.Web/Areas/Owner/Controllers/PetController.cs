namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Pets;

    [Authorize(Roles = GlobalConstants.OwnerRoleName + ", " + GlobalConstants.VetRoleName)]
    [Area("Owner")]
    public class PetController : Controller
    {
        private readonly IPetsService petsService;
        private readonly IOwnersService ownersService;
        private readonly IVetsService vetsService;
        private readonly ICloudinaryService cloudinaryService;

        public PetController(IPetsService petsService, IOwnersService ownersService, IVetsService vetsService, ICloudinaryService cloudinaryService)
        {
            this.petsService = petsService;
            this.ownersService = ownersService;
            this.vetsService = vetsService;
            this.cloudinaryService = cloudinaryService;
        }

        public IActionResult Details(string id)
        {
            var model = this.petsService.GetById<PetViewModel>(id);
            if (this.User.IsInRole(GlobalConstants.OwnerRoleName))
            {
                string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (currentUserId != model.Owner.UserId)
                {
                    this.TempData["InvalidPetRquest"] = "This is not your pet! You are not allowed to see information about other people's pets.";
                    return this.RedirectToAction("MyPets", "Owner", new { area = "Owner" });
                }

            }
            return this.View(model);
        }

        [Authorize(Roles = GlobalConstants.OwnerRoleName)]
        public IActionResult AddPet()
        {
            var vets = this.vetsService.GetAll<VetDropDown>();
            var model = new AddPetInputModel();
            model.Vet = vets;
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.OwnerRoleName)]
        public async Task<IActionResult> AddPet(AddPetInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                IEnumerable<ModelError> allErrors = this.ModelState.Values.SelectMany(v => v.Errors);
                return this.View("ModelStateError", allErrors);
            }

            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(currentUserId);
            string photoUrl = await this.petsService.DeterminePhotoUrl(model.Picture, model.Type);
            try
            {
                await this.petsService.AddPetAsync(ownerId, model, photoUrl);
                return this.RedirectToAction("MyPets", "Owner");
            }
            catch (ArgumentException ex)
            {
                return this.View("Error");
            }
        }
    }
}
