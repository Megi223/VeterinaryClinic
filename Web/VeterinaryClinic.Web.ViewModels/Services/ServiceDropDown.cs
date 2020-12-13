namespace VeterinaryClinic.Web.ViewModels.Services
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class ServiceDropDown : IMapFrom<Service>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
