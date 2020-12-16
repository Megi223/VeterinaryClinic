namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
    using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Pets;

    [Authorize(Roles = GlobalConstants.OwnerRoleName + ", " + GlobalConstants.VetRoleName)]
    [Area("Owner")]
    public class PetController : Controller
    {
        private readonly IPetsService petsService;
        private readonly IOwnersService ownersService;
        private readonly IVetsService vetsService;
        private readonly ComputerVisionClient computerVisionClient;

        public PetController(IPetsService petsService, IOwnersService ownersService, IVetsService vetsService, ComputerVisionClient computerVisionClient)
        {
            this.petsService = petsService;
            this.ownersService = ownersService;
            this.vetsService = vetsService;
            this.computerVisionClient = computerVisionClient;
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
            if (model.Picture != null)
            {
                List<VisualFeatureTypes?> features = new List<VisualFeatureTypes?>()
                {
                    VisualFeatureTypes.Categories, VisualFeatureTypes.Description,
                    VisualFeatureTypes.Faces, VisualFeatureTypes.ImageType,
                    VisualFeatureTypes.Tags, VisualFeatureTypes.Adult,
                    VisualFeatureTypes.Color, VisualFeatureTypes.Brands,
                    VisualFeatureTypes.Objects,
                };
                var stream = new MemoryStream();
                model.Picture.CopyTo(stream);
                var fileBytes = stream.ToArray();
                var actualStream = new MemoryStream(fileBytes);
                ImageAnalysis results = await this.computerVisionClient.AnalyzeImageInStreamAsync(actualStream, features);
                var tagNames = results.Tags.Select(x => x.Name).ToList();
                var typeOfAnimal = Enum.Parse(typeof(TypeOfAnimal), model.Type, true).ToString();

                if (!tagNames.Contains(typeOfAnimal.ToLower()))
                {
                    this.TempData["InvalidPicture"] = "The provided picture is not valid! Please ensure that it meets the type of animal you are trying to add.";
                    var vets = this.vetsService.GetAll<VetDropDown>();
                    model.Vet = vets;
                    return this.View(model);
                }
            }

            if (!this.ModelState.IsValid)
            {
                this.TempData["InvalidModelState"] = "Invalid data!";
                var vets = this.vetsService.GetAll<VetDropDown>();
                model.Vet = vets;
                return this.View(model);
            }

            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(currentUserId);
            string photoUrl = await this.petsService.DeterminePhotoUrl(model.Picture, model.Type);
            try
            {
                await this.petsService.AddPetAsync(ownerId, model, photoUrl);
                return this.RedirectToAction("MyPets", "Owner");
            }
            catch (ArgumentException)
            {
                this.TempData["InvalidIdentificationNumber"] = "Invalid identification number!";
                var vets = this.vetsService.GetAll<VetDropDown>();
                model.Vet = vets;
                return this.View(model);
            }
        }

        [Authorize(Roles = GlobalConstants.OwnerRoleName)]
        public IActionResult Edit(string id)
        {
            var vets = this.vetsService.GetAll<VetDropDown>();
            var model = this.petsService.GetById<EditPetViewModel>(id);
            model.Vet = vets;
            return this.View(model);
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.OwnerRoleName)]
        public async Task<IActionResult> Edit(EditPetViewModel edit)
        {
            if (!this.ModelState.IsValid)
            {
                var vets = this.vetsService.GetAll<VetDropDown>();
                edit.Vet = vets;
                return this.View(edit);
            }

            await this.petsService.EditAsync(edit);
            return this.RedirectToAction("MyPets", "Owner", new { area = "Owner" });
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.OwnerRoleName)]
        public async Task<IActionResult> Delete(string id)
        {
            await this.petsService.DeleteAsync(id);
            this.TempData["SuccessfulDeletion"] = "Successful deletion";
            return this.RedirectToAction("MyPets", "Owner", new { area = "Owner" });
        }
    }
}
