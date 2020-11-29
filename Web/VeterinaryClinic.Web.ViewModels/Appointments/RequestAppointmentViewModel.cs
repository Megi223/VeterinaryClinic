using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Common;
using VeterinaryClinic.Web.ViewModels.Pets;

namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    public class RequestAppointmentViewModel
    {
        [Required]
        [Display(Name = "Pet")]
        public string PetId { get; set; }

        public IEnumerable<PetDropDown> Pets { get; set; }

        [Required]
        [MaxLength(GlobalConstants.AppointmentSubjectMaxLength)]
        public string Subject { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [Required]
        [Display(Name = "Vet")]
        public string VetId { get; set; }

        public IEnumerable<VetDropDown> Vets { get; set; }
    }
}
