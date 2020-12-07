using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Notifications;

namespace VeterinaryClinic.Web.Controllers
{
    [Authorize(Roles = GlobalConstants.OwnerRoleName + ", " + GlobalConstants.VetRoleName)]
    public class NotificationsController : Controller
    {
        private readonly INotificationsService notificationsService;
        private readonly IOwnersService ownersService;
        private readonly IVetsService vetsService;


        public NotificationsController(INotificationsService notificationsService, IOwnersService ownersService, IVetsService vetsService)
        {
            this.notificationsService = notificationsService;
            this.ownersService = ownersService;
            this.vetsService = vetsService;
        }
        
        public IActionResult MyNotifications()
        {
            var viewModel = new List<NotificationViewModel>();
            string currentUserId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (this.User.IsInRole(GlobalConstants.OwnerRoleName))
            {
                string ownerId = this.ownersService.GetOwnerId(currentUserId);
                viewModel = this.notificationsService.GetOwnerNotifications<NotificationViewModel>(ownerId);

            }
            else
            {
                string vetId = this.vetsService.GetVetId(currentUserId);
                viewModel = this.notificationsService.GetVetNotifications<NotificationViewModel>(vetId);
            }

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.notificationsService.Delete(id);
            return this.RedirectToAction("MyNotifications");
        }
    }
}
