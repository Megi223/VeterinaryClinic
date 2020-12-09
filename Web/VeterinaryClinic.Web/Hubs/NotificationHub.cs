using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Common;
using VeterinaryClinic.Services.Data;
using VeterinaryClinic.Web.ViewModels.Notifications;

namespace VeterinaryClinic.Web.Hubs
{
    [Authorize(Roles = GlobalConstants.OwnerRoleName + ", " + GlobalConstants.VetRoleName)]
    public class NotificationHub : Hub
    {
        private readonly INotificationsService notificationsService;

        public NotificationHub(INotificationsService notificationsService)
        {
            this.notificationsService = notificationsService;
        }

        public async Task SendNotification(SendNotificationViewModel notification)
        {
            await this.Clients.Caller.SendAsync("SendNotification", notification);
        }

        public async Task ReceiveNotification(string vetId)
        {
            //this.notificationsService.GetVetNotifications
        }
    }
}
