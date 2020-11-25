using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VeterinaryClinic.Data.Common.Models;

namespace VeterinaryClinic.Data.Models
{
    public class PetsMedications : BaseDeletableModel<int>
    {
        public int MedicationId { get; set; }

        public virtual Medication Medication { get; set; }

        [Required]
        public string PetId { get; set; }

        public virtual Pet Pet { get; set; }

    }
}
