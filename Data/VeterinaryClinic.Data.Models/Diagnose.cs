namespace VeterinaryClinic.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;

    public class Diagnose : BaseDeletableModel<int>
    {
        public Diagnose()
        {
            this.Medications = new HashSet<Medication>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public virtual ICollection<Medication> Medications { get; set; }

    }
}
