namespace VeterinaryClinic.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.Controllers;
    using VeterinaryClinic.Web.ViewModels.Services;
    using VeterinaryClinic.Web.ViewModels.Vets;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly INewsScraperService newsScraperService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IVetsService vetsService;
        private readonly IServicesService servicesServices;


        public AdministrationController(INewsScraperService newsScraperService, UserManager<ApplicationUser> userManager, IVetsService vetsService, IServicesService servicesServices)
        {
            this.newsScraperService = newsScraperService;
            this.userManager = userManager;
            this.vetsService = vetsService;
            this.servicesServices = servicesServices;
        }

        public IActionResult AddVet()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddVet(AddVetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["InvalidVet"] = "Invalid data!";
                return this.View();
            }

            var user = new ApplicationUser { UserName = input.UserName, Email = input.Email, PhoneNumber = input.PhoneNumber };
            var result = await this.userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                string photoUrl = await this.vetsService.DeterminePhotoUrl(input.ProfilePicture);
                await this.vetsService.AddVetAsync(user.Id, input, photoUrl);
                await this.userManager.AddToRoleAsync(user, GlobalConstants.VetRoleName);
            }

            return this.RedirectToAction("Index", "Home", new { area = string.Empty });

        }

        public IActionResult AddServiceToVet(string id)
        {
            var vetName = this.vetsService.GetNameById(id);
            var services = this.servicesServices.GetAllServicesWhichAVetDoesNotHave<ServiceDropDown>(id);
            var viewModel = new AddServiceToVetViewModel();
            viewModel.VetId = id;
            viewModel.VetName = vetName;
            viewModel.Services = services;
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddServiceToVet(AddServiceToVetInputModel input)
        {
            await this.servicesServices.AddServiceToVet(input);
            return this.RedirectToAction("All","Vet",new { area="Vet" });
        }
    }
}
