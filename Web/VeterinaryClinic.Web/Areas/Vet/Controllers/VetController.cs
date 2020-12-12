namespace VeterinaryClinic.Web.Areas.Vet.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Pioneer.Pagination;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.ChatMessages;
    using VeterinaryClinic.Web.ViewModels.Owners;
    using VeterinaryClinic.Web.ViewModels.Pets;
    using VeterinaryClinic.Web.ViewModels.Vets;

    [Authorize(Roles = GlobalConstants.VetRoleName)]
    [Area("Vet")]
    public class VetController : Controller
    {
        private readonly IVetsService vetsService;
        private readonly IOwnersService ownersService;
        private readonly IPaginatedMetaService paginatedMetaService;

        public VetController(IVetsService vetsService, IPaginatedMetaService paginatedMetaService, IOwnersService ownersService)
        {
            this.vetsService = vetsService;
            this.paginatedMetaService = paginatedMetaService;
            this.ownersService = ownersService;
        }

        [AllowAnonymous]
        public IActionResult All(int id = 1)
        {
            var vetsCount = this.vetsService.GetCount();
            var allPagesCount = (vetsCount / GlobalConstants.VetsOnOnePage) + 1;
            if (id < 1)
            {
                return this.RedirectToAction("All", new { id = 1 });
            }
            else if (id > allPagesCount)
            {
                return this.RedirectToAction("All", new { id = allPagesCount });
            }

            var viewModel = this.vetsService.GetAllForAPage<VetsAllViewModel>(id);
            foreach (var vetModel in viewModel)
            {
                vetModel.Services = this.vetsService.GetServices(vetModel.Id);
            }

            this.ViewBag.PaginatedMeta = this.paginatedMetaService.GetMetaData(vetsCount, id, GlobalConstants.VetsOnOnePage);
            return this.View(viewModel);
        }

        [AllowAnonymous]
        public IActionResult Details(string id)
        {
            var viewModel = this.vetsService.GetById<VetViewModel>(id);
            viewModel.Services = this.vetsService.GetServices(viewModel.Id);

            return this.View(viewModel);
        }

        public IActionResult MyPatients(int id = 1)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string vetId = this.vetsService.GetVetId(userId);
            var vetsPatientsCount = this.vetsService.GetPatientsCount(vetId);
            var allPagesCount = (vetsPatientsCount / GlobalConstants.VetsPatientsOnOnePage) + 1;
            if (id < 1)
            {
                return this.RedirectToAction("All", new { id = 1 });
            }
            else if (id > allPagesCount)
            {
                return this.RedirectToAction("All", new { id = allPagesCount });
            }

            var viewModel = this.vetsService.GetVetsPatientsForAPage<AllPetsViewModel>(vetId,id);
            this.ViewBag.PaginatedMeta = this.paginatedMetaService.GetMetaData(vetsPatientsCount, id, GlobalConstants.VetsPatientsOnOnePage);
            return this.View(viewModel);
        }

        public IActionResult Chat(string id)
        {
            ChatPageViewModel model = new ChatPageViewModel();
            var owner = this.ownersService.GetById<OwnerViewModel>(id);
            model.OwnerId = id;
            model.OwnerFullName = owner.FullName;
            model.OwnerProfilePicture = owner.ProfilePicture;
            model.OwnerUserId = owner.UserId;

            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var vetId = this.vetsService.GetVetId(currentUserId);

            var vet = this.vetsService.GetById<VetViewModel>(vetId);
            model.VetFullName = vet.Name;
            model.VetId = vetId;
            model.VetProfilePicture = vet.ProfilePicture;
            model.VetUserId = vet.UserId;

            return this.View("~/Views/Chat/Chat.cshtml", model);
        }

        public IActionResult StartChat()
        {
            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var vetId = this.vetsService.GetVetId(currentUserId);
            var model = this.vetsService.GetVetsPatients<SelectChatViewModel>(vetId).ToList().GroupBy(x => x.OwnerFullName);

            return this.View(model);
        }
    }
}
