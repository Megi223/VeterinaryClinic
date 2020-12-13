namespace VeterinaryClinic.Web.ViewModels.Notifications
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class SendNotificationViewModel : IMapFrom<Notification>
    {
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        public string Content { get; set; }
    }
}
