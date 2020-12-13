namespace VeterinaryClinic.Web.Hubs
{
    using System.Linq;
    using System.Threading.Tasks;

    using Ganss.XSS;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.ChatMessages;
    using VeterinaryClinic.Web.ViewModels.Owners;
    using VeterinaryClinic.Web.ViewModels.Vets;

    [Authorize(Roles = GlobalConstants.OwnerRoleName + ", " + GlobalConstants.VetRoleName)]
    public class ChatHub : Hub
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IOwnersService ownersService;
        private readonly IVetsService vetsService;

        public ChatHub(UserManager<ApplicationUser> userManager, IOwnersService ownersService, IVetsService vetsService)
        {
            this.userManager = userManager;
            this.ownersService = ownersService;
            this.vetsService = vetsService;
        }

        public async Task Send(SendChatMessageInputModel inputModel)
        {
            var sanitizer = new HtmlSanitizer();
            var message = sanitizer.Sanitize(inputModel.Message);

            if (string.IsNullOrEmpty(message) ||
                string.IsNullOrWhiteSpace(message))
            {
                return;
            }

            var caller = string.Empty;
            string name = string.Empty;

            if (await this.userManager.IsInRoleAsync(this.userManager.Users.First(x => x.Id == inputModel.CallerId), GlobalConstants.OwnerRoleName))
            {
                string ownerId = this.ownersService.GetOwnerId(inputModel.CallerId);
                caller = this.ownersService.GetById<OwnerViewModel>(ownerId).ProfilePicture;
                name = this.ownersService.GetById<OwnerViewModel>(ownerId).FullName;
            }
            else if (await this.userManager.IsInRoleAsync(this.userManager.Users.First(x => x.Id == inputModel.CallerId), GlobalConstants.VetRoleName))
            {
                string vetId = this.vetsService.GetVetId(inputModel.CallerId);
                caller = this.vetsService.GetById<VetViewModel>(vetId).ProfilePicture;
                name = this.vetsService.GetById<VetViewModel>(vetId).Name;
            }

            await this.Clients
                .User(inputModel.UserId)
                .SendAsync("RecieveMessage", message, caller, name);

            await this.Clients
                .Caller
                .SendAsync("SendMessage", message, caller, name);
        }
    }
}
