namespace VeterinaryClinic.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    using System.Threading.Tasks;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.Controllers;
    using VeterinaryClinic.Web.ViewModels.News;
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
        private readonly INewsService newsService;
        private readonly IServiceScraperService serviceScraperService;

        public AdministrationController(INewsScraperService newsScraperService, UserManager<ApplicationUser> userManager, IVetsService vetsService, IServicesService servicesServices, IServiceScraperService serviceScraperService, INewsService newsService)
        {
            this.newsScraperService = newsScraperService;
            this.userManager = userManager;
            this.vetsService = vetsService;
            this.servicesServices = servicesServices;
            this.serviceScraperService = serviceScraperService;
            this.newsService = newsService;
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
            return this.RedirectToAction("All", "Vet", new { area = "Vet" });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteVet(string id)
        {
            await this.vetsService.DeleteVet(id);
            return this.RedirectToAction("All", "Vet", new { area = "Vet" });
        }

        public IActionResult EditVet(string id)
        {
            var model = this.vetsService.GetById<EditVetInputModel>(id);
            model.Services = this.vetsService.GetServices<EditVetsServicesDropDown>(id);
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditVet(EditVetInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var model = this.vetsService.GetById<EditVetInputModel>(input.Id);
                model.Services = this.vetsService.GetServices<EditVetsServicesDropDown>(input.Id);
                return this.View(input);
            }

            await this.vetsService.EditVet(input);
            return this.RedirectToAction("Details", "Vet", new { area = "Vet", id = input.Id });
        }

        public async Task<IActionResult> AddServices()
        {
            await this.serviceScraperService.PopulateDbWithServices();
            return this.RedirectToAction("Index");
        }

        public IActionResult AddNews()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNews(AddNewsInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["InvalidNews"] = "Invalid data!";
                return this.View(input);
            }

            await this.newsService.AddNewsAsync(input);
            return this.RedirectToAction("All", "News", new { area = string.Empty });
        }
    }
}
