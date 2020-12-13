namespace VeterinaryClinic.Web.ViewModels.Appointments
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Models;
    using VeterinaryClinic.Services.Mapping;

    public class AppointmentInProgressViewModel : IMapFrom<Appointment>
    {
        public string Id { get; set; }

        public string OwnerFirstName { get; set; }

        public string OwnerLastName { get; set; }

        public string OwnerFullName => this.OwnerFirstName + " " + this.OwnerLastName;

        public string PetName { get; set; }

        public string PetId { get; set; }

        public string VetId { get; set; }

        public string Subject { get; set; }

        public DateTime StartTime { get; set; }

        [Display(Name = "Description")]
        [Required]
        public string PetDiagnoseDescription { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string PetDiagnoseName { get; set; }

        public ICollection<PetsMedicationsViewModel> PetPetsMedications { get; set; }
    }
}
