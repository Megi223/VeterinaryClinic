namespace VeterinaryClinic.Web.Areas.Vet.Controllers
{
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.ChatMessages;

    [Route("api/[controller]")]
    [ApiController]
    public class MessagesApiController : Controller
    {
        private readonly IChatMessagesService chatMessagesService;
        private readonly IOwnersService ownersService;
        private readonly IVetsService vetsService;

        public MessagesApiController(IChatMessagesService chatMessagesService, IOwnersService ownersService, IVetsService vetsService)
        {
            this.chatMessagesService = chatMessagesService;
            this.ownersService = ownersService;
            this.vetsService = vetsService;
        }

        [HttpPost]
        [Route("/api/Messages")]
        public async Task<IActionResult> Post(SendChatMessageInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.NoContent();
            }

            var sanitizer = new HtmlSanitizer();
            var message = sanitizer.Sanitize(model.Message);

            if (this.User.IsInRole(GlobalConstants.OwnerRoleName))
            {
                var senderRoleName = RoleName.Owner;
                var receiverRoleName = RoleName.Vet;
                var ownerIdSender = this.ownersService.GetOwnerId(model.CallerId);
                var vetReceiverId = this.vetsService.GetVetId(model.UserId);

                await this.chatMessagesService.CreateAsync(senderRoleName, receiverRoleName, ownerIdSender, vetReceiverId, message);
                return this.Ok();
            }

            var senderRoleNameVet = RoleName.Vet;
            var receiverRoleNameOwner = RoleName.Owner;
            var ownerIdReceiver = this.ownersService.GetOwnerId(model.UserId);
            var vetSenderId = this.vetsService.GetVetId(model.CallerId);

            await this.chatMessagesService.CreateAsync(senderRoleNameVet, receiverRoleNameOwner, ownerIdReceiver, vetSenderId, message);

            return this.Ok();
        }
    }
}
