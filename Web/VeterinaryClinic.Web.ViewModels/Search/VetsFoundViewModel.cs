﻿using AutoMapper;
using System.Linq;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Search
{
    public class VetsFoundViewModel : IMapFrom<Vet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string Services { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Vet, VetsFoundViewModel>()
                .ForMember(x => x.Services, opt =>
                  opt.MapFrom(x => "Services: " + string.Join(",", x.VetsServices.Select(x => x.Service.Name))));
        }
    }
}