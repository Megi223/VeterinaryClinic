using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Web.ViewModels.Services;

namespace VeterinaryClinic.Web.ViewModels.Vets
{
    public class AddServiceToVetInputModel
    {
        public string VetId { get; set; }

        public IEnumerable<int> Services { get; set; }
    }
}
