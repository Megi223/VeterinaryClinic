namespace VeterinaryClinic.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using VeterinaryClinic.Data.Common.Models;

    public class Rating : BaseDeletableModel<int>
    {
        [Required]
        public string OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        [Required]
        public string DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public float Score { get; set; }
    }
}
