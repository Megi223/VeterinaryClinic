using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Common;

namespace VeterinaryClinic.Web.Hubs
{
    [Authorize(Roles = GlobalConstants.OwnerRoleName + ", " + GlobalConstants.VetRoleName)]
    public class NotificationHub : Hub
    {
        public async Task SendNotification(string message)
        {
            await this.Clients.Caller.SendAsync("SendNotification", message);
        }
    }
}
