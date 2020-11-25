using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Web.Infrastructure.Attributes;
//using System.Web.Mvc;

namespace VeterinaryClinic.Web.ViewModels.Pets
{
    public class AddPetInputModel : IMapTo<Pet>
    {
        //make all the necessary validation

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Display(Name="Vet")]
        public string VetId { get; set; }

        public IEnumerable<VetDropDown> Vet { get; set; }

        [Range(0.01, 100)]
        public float Weight { get; set; }

        public IFormFile Picture { get; set; }

        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "Please enter a valid Identification number- it should contain only alphanumerical characters")]
        public string IdentificationNumber { get; set; }

        [DataType(DataType.Date)]
        [MyValidDateAttribute]
        public DateTime? Birthday { get; set; }

        [Required]
        public string Sterilised { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
