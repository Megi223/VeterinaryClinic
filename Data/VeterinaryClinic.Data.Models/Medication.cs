namespace VeterinaryClinic.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common;
    using VeterinaryClinic.Data.Common.Models;

    public class Medication : BaseDeletableModel<int>
    {
        public Medication()
        {
            this.DosingTimes = new HashSet<DosingTime>();
        }

        [Required]
        [MaxLength(DataValidationConstants.MedicationNameMaxLength)]
        public string Name { get; set; }

        public int NumberOfDosesPerServing { get; set; }

        public virtual ICollection<DosingTime> DosingTimes { get; set; }
    }
}
