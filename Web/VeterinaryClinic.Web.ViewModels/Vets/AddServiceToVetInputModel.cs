namespace VeterinaryClinic.Web.ViewModels.Vets
{
    using System.Collections.Generic;

    public class AddServiceToVetInputModel
    {
        public string VetId { get; set; }

        public IEnumerable<int> Services { get; set; }
    }
}
