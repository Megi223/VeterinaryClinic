namespace VeterinaryClinic.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class Medication : BaseDeletableModel<int>
    {
        public Medication()
        {
            this.PetsMedications = new HashSet<PetsMedications>();
        }

        [Required]
        [MaxLength(GlobalConstants.MedicationNameMaxLength)]
        public string Name { get; set; }

        [Required]
        public string NumberOfDosesPerServing { get; set; }

        public virtual ICollection<PetsMedications> PetsMedications { get; set; }
    }
}
