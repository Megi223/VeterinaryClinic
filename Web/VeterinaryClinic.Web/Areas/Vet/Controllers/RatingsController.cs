using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Ratings;

namespace VeterinaryClinic.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly IRatingService ratingService;
        private readonly UserManager<ApplicationUser> userManager;

        public RatingsController(IRatingService ratingService, UserManager<ApplicationUser> userManager)
        {
            this.ratingService = ratingService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles=GlobalConstants.OwnerRoleName)]
        [Route("/api/Ratings")]
        public async Task<ActionResult<PostRatingResponseModel>> Post(PostRatingInputModel input)
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);
            var ownerId = currentUser.Owner.Id;
            await this.ratingService.SetRatingAsync(input.VetId, ownerId, input.Score);
            var averageRatings = this.ratingService.GetAverageRatings(input.VetId);
            return new PostRatingResponseModel { AverageRating = averageRatings };
        }
    }
}
