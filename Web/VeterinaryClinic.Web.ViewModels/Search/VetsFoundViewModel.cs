﻿namespace VeterinaryClinic.Web.ViewModels.Search
{
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class VetsFoundViewModel : IMapFrom<Vet>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePicture { get; set; }

        public string Services { get; set; }

        /*public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Vet, VetsFoundViewModel>()
                .ForMember(x => x.Services, opt =>
                  opt.MapFrom(x => "Services: " + string.Join(",", x.VetsServices.Select(x => x.Service.Name))));
        }*/
    }
}
