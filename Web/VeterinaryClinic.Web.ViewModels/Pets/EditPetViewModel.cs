using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Data.Models.Enumerations;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Web.Infrastructure.Attributes;

namespace VeterinaryClinic.Web.ViewModels.Pets
{
    public class EditPetViewModel : IMapFrom<Pet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string Name { get; set; }


        [Required]
        [Display(Name = "Vet")]
        public string VetId { get; set; }

        [IgnoreMap]
        public IEnumerable<VetDropDown> Vet { get; set; }

        [Range(0.01, 100)]
        public float Weight { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [IgnoreMap]
        public IFormFile NewPicture { get; set; }

        [Required]
        public string Sterilised { get; set; }


        public string PictureFromDatabase { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Pet, EditPetViewModel>()
                .ForMember(x => x.PictureFromDatabase, opt => opt.MapFrom(x => x.Picture));
            
        }
    }
}
