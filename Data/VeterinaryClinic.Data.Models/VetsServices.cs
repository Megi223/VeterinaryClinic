namespace VeterinaryClinic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;

    public class VetsServices : BaseDeletableModel<int>
    {
        [Required]
        public string VetId { get; set; }

        public virtual Vet Vet { get; set; }

        [Required]
        public string ServiceId { get; set; }

        public virtual Service Service { get; set; }
    }
}
