using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;
using VeterinaryClinic.Services.Data;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using VeterinaryClinic.Web.ViewModels.ChatMessages;

namespace VeterinaryClinic.Web.ViewComponents
{
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
