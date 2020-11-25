using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;

namespace VeterinaryClinic.Web.ViewModels.Pets
{
    public class AllPetsViewModel : IMapFrom<Pet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Picture { get; set; }

        public DateTime Birthday { get; set; }

        public int Age => this.CalculateAge(this.Birthday, DateTime.UtcNow);

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, AllPetsViewModel>()
                .ForMember(x => x.Gender, opt => opt.MapFrom(p => p.Gender.ToString()));
        }

        private int CalculateAge(DateTime start, DateTime end)
        {
            return (end.Year - start.Year - 1) +
                (((end.Month > start.Month) ||
                ((end.Month == start.Month) && (end.Day >= start.Day))) ? 1 : 0);
        }
    }
}
