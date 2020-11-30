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
    using VeterinaryClinic.Web.ViewModels.Pets;
    using VeterinaryClinic.Web.ViewModels.Reviews;

    [Authorize(Roles = GlobalConstants.OwnerRoleName)]
    [Area("Owner")]
    public class OwnerController : Controller
    {
        private const int PetsOnOnePage = 3;
        private readonly IPetsService petsService;
        private readonly IOwnersService ownersService;
        private readonly IPaginatedMetaService paginatedMetaService;
        private readonly UserManager<ApplicationUser> userManager;

        public OwnerController(IPetsService petsService, IOwnersService ownersService, IPaginatedMetaService paginatedMetaService, UserManager<ApplicationUser> userManager)
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
    }
}
