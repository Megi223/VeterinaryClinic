namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class PetDropDown : IMapFrom<Pet>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
