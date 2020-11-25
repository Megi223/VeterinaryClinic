using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Pets
{
    public class VetDropDown : IMapFrom<Vet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Vet, VetDropDown>()
                .ForMember(x => x.Name, opt => opt.MapFrom(v => v.FirstName + " " + v.LastName));
        }
    }
}
