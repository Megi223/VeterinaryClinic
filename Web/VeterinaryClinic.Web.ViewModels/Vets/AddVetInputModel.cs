using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Common;
using VeterinaryClinic.Web.Infrastructure.Attributes;

namespace VeterinaryClinic.Web.ViewModels.Vets
{
    public class AddVetInputModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(GlobalConstants.NameMaxLength)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        [Display(Name ="Profile Picture")]
        public IFormFile ProfilePicture { get; set; }

        [Required]
        [MaxLength(GlobalConstants.SpecializationMaxLength)]
        public string Specialization { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [MyValidDateAttribute(ErrorMessage = "Invalid date!")]
        public DateTime HireDate { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
