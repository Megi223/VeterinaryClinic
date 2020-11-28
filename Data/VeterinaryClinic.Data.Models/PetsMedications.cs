namespace VeterinaryClinic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;

    public class PetsMedications : BaseDeletableModel<int>
    {
        public int MedicationId { get; set; }

        public virtual Medication Medication { get; set; }

        [Required]
        public string PetId { get; set; }

        public virtual Pet Pet { get; set; }
    }
}
