using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Web.ViewModels.Services;

namespace VeterinaryClinic.Web.ViewModels.Vets
{
    public class AddServiceToVetViewModel
    {
        public string VetId { get; set; }

        public string VetName { get; set; }

        public IEnumerable<ServiceDropDown> Services { get; set; }
    }
}
