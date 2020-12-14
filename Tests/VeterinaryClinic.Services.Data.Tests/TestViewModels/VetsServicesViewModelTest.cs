using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Services.Data.Tests.TestViewModels
{
    public class VetsServicesViewModelTest : IMapFrom<VetsServices>
    {
        public string VetId { get; set; }

        public string ServiceId { get; set; }
    }
}
