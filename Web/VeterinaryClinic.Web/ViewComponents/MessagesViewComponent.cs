namespace VeterinaryClinic.Web.ViewComponents
{
    using Microsoft.AspNetCore.Mvc;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models.Enumerations;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.ChatMessages;

    public class MessagesViewComponent : ViewComponent
    {
        private readonly IChatMessagesService chatMessagesService;

        public MessagesViewComponent(IChatMessagesService chatMessagesService)
        {
            this.chatMessagesService = chatMessagesService;
        }

        public IViewComponentResult Invoke(string userId)
        {
            RoleName roleName = this.User.IsInRole(GlobalConstants.OwnerRoleName) ? RoleName.Owner : RoleName.Vet;

            var model = this.chatMessagesService.GetLatestChatMessages<LatestMessagesViewModel>(roleName, userId);
            return this.View(model);
        }
    }
}
