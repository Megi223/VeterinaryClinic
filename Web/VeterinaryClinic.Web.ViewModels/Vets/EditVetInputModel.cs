using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Common;
using VeterinaryClinic.Data.Models;
using VeterinaryClinic.Services.Mapping;
using VeterinaryClinic.Web.ViewModels.Services;

namespace VeterinaryClinic.Web.ViewModels.Vets
{
    public class EditVetInputModel : IMapFrom<Vet>
    {
        public string Id { get; set; }

        [Display(Name = "Profile Picture")]
        public IFormFile Picture { get; set; }

        [Required]
        [MaxLength(GlobalConstants.SpecializationMaxLength)]
        public string Specialization { get; set; }

        public IEnumerable<EditVetsServicesDropDown> Services { get; set; }

        public IEnumerable<string> ServicesInput { get; set; }
    }
}
