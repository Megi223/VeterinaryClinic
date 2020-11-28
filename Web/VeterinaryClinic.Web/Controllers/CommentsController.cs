namespace VeterinaryClinic.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Comments;

    [Authorize(Roles = GlobalConstants.OwnerRoleName + "," + GlobalConstants.VetRoleName)]
    public class CommentsController : Controller
    {
        private readonly ICommentsService commentsService;
        private readonly IOwnersService ownersService;

        private readonly UserManager<ApplicationUser> userManager;

        public CommentsController(
            ICommentsService commentsService,
            UserManager<ApplicationUser> userManager,
            IOwnersService ownersService)
        {
            this.commentsService = commentsService;
            this.userManager = userManager;
            this.ownersService = ownersService;
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCommentInputModel input)
        {
            var parentId =
                input.ParentId == 0 ?
                    (int?)null :
                    input.ParentId;

            if (parentId.HasValue)
            {
                if (!this.commentsService.IsInCorrectVetId(parentId.Value, input.VetId))
                {
                    return this.RedirectToAction("Error", "Home", new { area = string.Empty });
                }
            }

            var userId = this.userManager.GetUserId(this.User);
            var ownerId = this.ownersService.GetOwnerId(userId);
            await this.commentsService.AddAsync(input.VetId, ownerId, input.Content, parentId);
            return this.RedirectToAction("Details", "Vet", new { id = input.VetId, area = "Vet" });
        }
    }
}
