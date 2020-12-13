namespace VeterinaryClinic.Web.ViewModels.Vets
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class EditVetsServicesDropDown : IMapFrom<VetsServices>
    {
        public string ServiceId { get; set; }

        public string ServiceName { get; set; }
    }
}
