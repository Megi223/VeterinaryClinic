namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class VetsServicesViewModelTest : IMapFrom<VetsServices>
    {
        public string VetId { get; set; }

        public string ServiceId { get; set; }
    }
}
