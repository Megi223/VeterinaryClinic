using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Notifications
{
    public class SendNotificationViewModel : IMapFrom<Notification>
    {
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public string Content { get; set; }
    }
}
