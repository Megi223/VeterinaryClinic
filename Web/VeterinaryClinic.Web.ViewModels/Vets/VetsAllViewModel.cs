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

        // public ICollection<VetsServices> VetsServices { get; set; }

        /* public void CreateMappings(IProfileExpression configuration)
         {
             //var vetsServices = this.VetsServices.Where(x => x.VetId == this.Id).Select(x => x.Service.Name);
             //TODO: Check this logic here
             configuration.CreateMap<Vet, VetsAllViewModel>()
                 .ForMember(x => x.Services, opt => opt.MapFrom(v => string.Join(", ", this.VetsServices.Select(x => x.Service.Name).ToList())));
         }*/
    }
}
