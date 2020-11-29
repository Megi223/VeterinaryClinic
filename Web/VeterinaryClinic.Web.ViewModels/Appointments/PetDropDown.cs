using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    public class PetDropDown : IMapFrom<Pet>
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}
