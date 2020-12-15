namespace VeterinaryClinic.Web.Controllers
{
    using System.Diagnostics;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Pioneer.Pagination;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels;
    using VeterinaryClinic.Web.ViewModels.Appointments;
    using VeterinaryClinic.Web.ViewModels.Pets;
    using VeterinaryClinic.Web.ViewModels.Reviews;

    public class HomeController : BaseController
    {
        private readonly IReviewsService reviewsService;
        private readonly IPaginatedMetaService paginatedMetaService;
        private readonly IVetsService vetsService;
        private readonly IOwnersService ownersService;
        private readonly IPetsService petsService;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(
            IReviewsService reviewsService, IPaginatedMetaService paginatedMetaService, IVetsService vetsService, IOwnersService ownersService, IPetsService petsService, UserManager<ApplicationUser> userManager)
        {
            this.reviewsService = reviewsService;
            this.paginatedMetaService = paginatedMetaService;
            this.vetsService = vetsService;
            this.ownersService = ownersService;
            this.petsService = petsService;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult About()
        {
            return this.View();
        }

        public IActionResult FAQ()
        {
            return this.View();
        }

        public IActionResult Contact()
        {
            if (this.User.Identity.IsAuthenticated && this.User.IsInRole(GlobalConstants.OwnerRoleName))
            {
                var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                string ownerId = this.ownersService.GetOwnerId(userId);
                var viewModel = new RequestAppointmentViewModel();
                var vets = this.vetsService.GetAll<VetDropDown>();
                viewModel.Vets = vets;
                var pets = this.petsService.GetPets<PetDropDown>(ownerId);
                viewModel.Pets = pets;
                return this.View("ContactAuthenticated", viewModel);
            }

            return this.View("Contact");
        }

        [HttpPost]
        [Authorize]
        public IActionResult Contact(string subject, string name, string petname, string vetname, string date, string time)
        {
            return this.RedirectToAction("Contact");
        }

        public async Task<IActionResult> StatusCodeError(int errorCode)
        {
            if (errorCode == 404)
            {
                return this.View("NotFound");
            }

            return this.RedirectToAction("Error", "Home");
        }

        public IActionResult AllReviews(int id = 1)
        {
            var reviewsCount = this.reviewsService.GetCount();
            var allPagesCount = (reviewsCount / GlobalConstants.ReviewsOnOnePage) + 1;
            if (id < 1)
            {
                return this.RedirectToAction("All", new { id = 1 });
            }
            else if (id > allPagesCount)
            {
                return this.RedirectToAction("All", new { id = allPagesCount });
            }

            var viewModel = this.reviewsService.GetAllForAPage<AllReviewsViewModel>(id);
            this.ViewBag.PaginatedMeta = this.paginatedMetaService.GetMetaData(reviewsCount, id, GlobalConstants.ReviewsOnOnePage);
            return this.View(viewModel);
        }
    }
}
