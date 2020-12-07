using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Notifications
{
    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string Content { get; set; }

    }
}
