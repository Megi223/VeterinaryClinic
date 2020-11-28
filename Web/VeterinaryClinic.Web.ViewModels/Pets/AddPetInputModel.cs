// using System.Web.Mvc;

namespace VeterinaryClinic.Web.ViewModels.Pets
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;
    using VeterinaryClinic.Common;
    using VeterinaryClinic.Web.Infrastructure.Attributes;

    public class AddPetInputModel
    {
        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Vet")]
        public string VetId { get; set; }

        public IEnumerable<VetDropDown> Vet { get; set; }

        [Range(0.01, 100)]
        public float Weight { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Picture { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z0-9_]+$", ErrorMessage = "Please enter a valid Identification number- it should contain only alphanumerical characters")]
        public string IdentificationNumber { get; set; }

        [DataType(DataType.Date)]
        [MyValidDateAttribute(ErrorMessage = "Invalid date!")]
        public DateTime? Birthday { get; set; }

        [Required]
        public string Sterilised { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
