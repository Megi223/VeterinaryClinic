namespace VeterinaryClinic.Web.Areas.Owner.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Pioneer.Pagination;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.ChatMessages;
    using VeterinaryClinic.Web.ViewModels.Owners;
    using VeterinaryClinic.Web.ViewModels.Pets;
    using VeterinaryClinic.Web.ViewModels.Reviews;
    using VeterinaryClinic.Web.ViewModels.Vets;

    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    //[Authorize(Roles = GlobalConstants.OwnerRoleName + ", " + GlobalConstants.VetRoleName)]
    [Area("Owner")]
    public class OwnerController : Controller
    {
        private const int PetsOnOnePage = 3;
        private readonly IPetsService petsService;
        private readonly IVetsService vetsService;
        private readonly IOwnersService ownersService;
        private readonly IPaginatedMetaService paginatedMetaService;
        private readonly UserManager<ApplicationUser> userManager;

        public OwnerController(IPetsService petsService, IOwnersService ownersService, IPaginatedMetaService paginatedMetaService, UserManager<ApplicationUser> userManager, IVetsService vetsService)
        {
            this.petsService = petsService;
            this.ownersService = ownersService;
            this.paginatedMetaService = paginatedMetaService;
            this.userManager = userManager;
            this.vetsService = vetsService;
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

        public IActionResult WriteReview()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> WriteReview(ReviewInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                this.TempData["Info"] = "Please write your review again!";
                return this.RedirectToAction("WriteReview");
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            string ownerId = this.ownersService.GetOwnerId(userId);
            await this.ownersService.WriteReviewAsync(ownerId, input);
            this.TempData["Message"] = "Thank You for Your feedback!";
            return this.RedirectToAction("AllReviews", "Home", new { area = string.Empty });
        }

        public IActionResult Chat(string id)
        {
            ChatPageViewModel model = new ChatPageViewModel();
            var vet = this.vetsService.GetById<VetViewModel>(id);
            model.VetFullName = vet.Name;
            model.VetId = id;
            model.VetProfilePicture = vet.ProfilePicture;
            model.VetUserId = vet.UserId;

            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var ownerId = this.ownersService.GetOwnerId(currentUserId);

            var owner = this.ownersService.GetById<OwnerViewModel>(ownerId);
            model.OwnerId = ownerId;
            model.OwnerFullName = owner.FullName;
            model.OwnerProfilePicture = owner.ProfilePicture;
            model.OwnerUserId = owner.UserId;
            return this.View("~/Views/Chat/Chat.cshtml", model);
        }
    }
}
