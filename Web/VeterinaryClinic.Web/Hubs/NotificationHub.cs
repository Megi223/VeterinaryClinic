namespace VeterinaryClinic.Web.Hubs
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.SignalR;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Services.Data;
    using VeterinaryClinic.Web.ViewModels.Notifications;

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
    }
}
