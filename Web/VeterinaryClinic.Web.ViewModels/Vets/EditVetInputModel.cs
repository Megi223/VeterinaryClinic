namespace VeterinaryClinic.Web.ViewModels.Vets
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

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
