namespace VeterinaryClinic.Web.ViewModels.Services
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class ServiceViewModel : IMapFrom<Service>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }
    }
}
