namespace VeterinaryClinic.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Ratings;

    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService ratingService;
        private readonly IOwnersService ownersService;

        private readonly UserManager<ApplicationUser> userManager;

        public RatingsController(IRatingService ratingService, UserManager<ApplicationUser> userManager, IOwnersService ownersService)
        {
            this.ratingService = ratingService;
            this.userManager = userManager;
            this.ownersService = ownersService;
        }

        [HttpPost]
        [Authorize(Roles = GlobalConstants.OwnerRoleName)]
        [Route("/api/Ratings")]
        public async Task<ActionResult<PostRatingResponseModel>> Post(PostRatingInputModel input)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var ownerId = this.ownersService.GetOwnerId(currentUser.Id);
            await this.ratingService.SetRatingAsync(input.VetId, ownerId, input.Score);
            var averageRatings = this.ratingService.GetAverageRatings(input.VetId);
            return new PostRatingResponseModel { AverageRating = averageRatings };
        }
    }
}
