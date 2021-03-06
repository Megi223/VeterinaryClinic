﻿namespace VeterinaryClinic.Web.ViewModels.Vets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class VetViewModel : IMapFrom<Vet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Name => this.FirstName + " " + this.LastName;

        public string ProfilePicture { get; set; }

        public string Specialization { get; set; }

        public DateTime HireDate { get; set; }

        public float AverageRating { get; set; }

        public string Services { get; set; }

        public IEnumerable<VetCommentViewModel> Comments { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Vet, VetViewModel>()
                .ForMember(x => x.AverageRating, opt =>
                    opt.MapFrom(x => x.Ratings.Count() == 0 ? 0 : x.Ratings.Average(v => v.Score)));
        }
    }
}
