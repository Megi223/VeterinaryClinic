using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.Areas.Owner.Models
{
    public class AllPetsViewModel : IMapFrom<Pet>
    {
        public string Name { get; set; }

        public string Breed { get; set; }

        public string Type { get; set; }
    }
}
