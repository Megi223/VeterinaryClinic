namespace VeterinaryClinic.Web.ViewModels.Vets
{
    using System.Collections.Generic;

    using VeterinaryClinic.Web.ViewModels.Services;

    public class AddServiceToVetViewModel
    {
        public string VetId { get; set; }

        public string VetName { get; set; }

        public IEnumerable<ServiceDropDown> Services { get; set; }
    }
}
