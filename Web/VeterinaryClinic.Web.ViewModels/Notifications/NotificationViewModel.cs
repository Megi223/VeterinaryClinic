namespace VeterinaryClinic.Web.ViewModels.Notifications
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class NotificationViewModel : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string Content { get; set; }
    }
}
