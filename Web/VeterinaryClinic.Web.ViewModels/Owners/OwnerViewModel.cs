namespace VeterinaryClinic.Web.ViewModels.Owners
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class OwnerViewModel : IMapFrom<Owner>
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;

        public string ProfilePicture { get; set; }
    }
}
