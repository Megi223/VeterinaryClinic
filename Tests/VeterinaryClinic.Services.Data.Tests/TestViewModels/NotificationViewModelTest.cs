namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class NotificationViewModelTest : IMapFrom<Notification>
    {
        public int Id { get; set; }

        public string VetId { get; set; }

        public string OwnerId { get; set; }

        public string Content { get; set; }
    }
}
