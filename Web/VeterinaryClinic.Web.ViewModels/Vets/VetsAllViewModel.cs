namespace VeterinaryClinic.Web.ViewModels.Vets
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class VetsAllViewModel : IMapFrom<Vet>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name => this.FirstName + " " + this.LastName;

        public string ProfilePicture { get; set; }

        public string Specialization { get; set; }

        public string Services { get; set; }

        
    }
}
