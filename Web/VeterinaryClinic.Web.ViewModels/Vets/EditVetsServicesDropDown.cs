using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Vets
{
    public class EditVetsServicesDropDown : IMapFrom<VetsServices>
    {
        public string ServiceId { get; set; }

        public string ServiceName { get; set; }
    }
}
